using FMath.Linear.Generic.Base;

namespace FMath.Linear.Generic.Mutable
{
    /// <summary>
    /// Mutable matrix that is implemented using an array.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MutableMatrixBase{TData}" />
    public class DenseMatrix<TData> :
        MutableMatrixBase<TData>
    {
        private readonly TData[,] FCells;

        /// <summary>
        /// Initializes a new instance of the <see cref="DenseMatrix{TData}"/> class.
        /// </summary>
        /// <param name="ASize">The size of the matrix.</param>
        public DenseMatrix(MatrixIndices ASize)
            : base(ASize)
        {
            this.FCells = new TData[ASize.M, ASize.N];
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DenseMatrix{TData}"/> class.
        /// </summary>
        /// <param name="ACells">The array of elements.</param>
        /// <param name="ACopy">If set to <c>true</c> a copy of the element array will be created, otherwise the reference will be assigned.</param>
        public DenseMatrix(TData[,] ACells, bool ACopy = true)
            : base(ACells != null ? new MatrixIndices(ACells.GetLength(0), ACells.GetLength(1)) : MatrixIndices.Zero)
        {
            if (ACells == null)
                this.FCells = new TData[0,0];
            else
                this.FCells = ACopy
                    ? (TData[,])ACells.Clone()
                    : ACells;
        }

        protected internal override TData DirectGet(MatrixIndices AIndices)
        {
            return this.FCells[AIndices.M, AIndices.N];
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TData AData)
        {
            this.FCells[AIndices.M, AIndices.N] = AData;
        }

        /// <inheritDoc />
        public override object Clone()
        {
            return new DenseMatrix<TData>(this.FCells);
        }
    }
}