using System.Collections.Generic;
using System.Linq;

using FMath.Linear.Generic.Base;
using FMath.Linear.Static;

namespace FMath.Linear.Generic.Mutable
{
    /// <summary>
    /// Mutable matrix that is implemented using a lookup.
    /// </summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <seealso cref="MutableMatrixBase{TData}" />
    public class SparseMatrix<TData> :
        MutableMatrixBase<TData>
    {
        private readonly TData FDefault;
        private readonly IEqualityComparer<TData> FComparer;

        protected readonly SortedList<MatrixIndices, TData> FCells;

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseMatrix{TData}"/> class.
        /// </summary>
        /// <param name="ASize">The size of the matrix.</param>
        /// <param name="ADefault">The default element.</param>
        /// <param name="AComparer">The element comparer.</param>
        /// <param name="ACells">An optional initializer for the cells.</param>
        public SparseMatrix(
            MatrixIndices ASize,
            TData ADefault = default(TData),
            IEqualityComparer<TData> AComparer = null,
            IDictionary<MatrixIndices, TData> ACells = null)
            : base(ASize)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            this.FDefault = ADefault;
            this.FComparer = AComparer;

            this.FCells = new SortedList<MatrixIndices, TData>();

            if (ACells != null)
                foreach (KeyValuePair<MatrixIndices, TData> kvpPair in ACells
                    .Where(APair => !AComparer.Equals(APair.Value, ADefault) && this.AreDefined(APair.Key)))
                    this.FCells.Add(kvpPair.Key, kvpPair.Value);
        }

        protected internal override TData DirectGet(MatrixIndices AIndices)
        {
            TData tOut;
            if (!this.FCells.TryGetValue(AIndices, out tOut))
                return this.FDefault;

            return tOut;
        }
        protected internal override void DirectSet(MatrixIndices AIndices, TData AData)
        {
            if (this.Comparer.Equals(AData, this.Default))
                this.FCells.Remove(AIndices);
            else
                this.FCells[AIndices] = AData;
        }

        /// <inheritDoc />
        public override object Clone()
        {
            return new SparseMatrix<TData>(this.Size, this.Default, this.Comparer, this.FCells);
        }

        /// <summary>
        /// Gets the default element.
        /// </summary>
        /// <value>
        /// The default element.
        /// </value>
        public TData Default
        {
            get { return this.FDefault; }
        }
        /// <summary>
        /// Gets the element comparer.
        /// </summary>
        /// <value>
        /// The element comparer.
        /// </value>
        public IEqualityComparer<TData> Comparer
        {
            get { return this.FComparer; }
        }
    }
}