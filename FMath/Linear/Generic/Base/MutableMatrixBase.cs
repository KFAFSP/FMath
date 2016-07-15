using System;
using System.Diagnostics.Contracts;

using FMath.Linear.Static;

namespace FMath.Linear.Generic.Base
{
    /// <summary>
    /// Abstract generic base class for mutable matrix types.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MatrixBase{TData}" />
    /// <seealso cref="IMutableMatrix{TData}" />
    /// <seealso cref="FMath.IAssignable{MatrixBase{TData}}" />
    public abstract class MutableMatrixBase<TData> :
        MatrixBase<TData>,
        IMutableMatrix<TData>,
        IAssignable<MatrixBase<TData>>
    {
        protected MutableMatrixBase(MatrixIndices ASize)
            : base(ASize)
        { }

        protected internal abstract void DirectSet(MatrixIndices AIndices, TData AData);

        #region IAssignable<IMatrix>
        /// <inheritDoc />
        public void Assign(IMatrix AFrom)
        {
            if (AFrom == null)
                throw new ArgumentNullException("AFrom");

            Matrix.Copy(AFrom, this);
        }
        #endregion

        #region IMutableMatrix
        /// <inheritDoc />
        void IMutableMatrix.Set(MatrixIndices AIndices, object AData)
        {
            if (!AData.Matches<TData>())
                throw new ArgumentException("Matrix type mismatch.");

            this.Set(AIndices, (TData)AData);
        }

        /// <inheritDoc />
        object IMutableMatrix.this[int ARow, int ACol]
        {
            [Pure]
            get { return ((IMatrix)this).Get(new MatrixIndices(ARow, ACol)); }
            set { ((IMutableMatrix)this).Set(new MatrixIndices(ARow, ACol), value); }
        }
        #endregion

        #region IMutableMatrix<TData>
        /// <inheritDoc />
        public void Set(MatrixIndices AIndices, TData AData)
        {
            if (!this.AreDefined(AIndices))
                throw new ArgumentOutOfRangeException("AIndices");

            this.DirectSet(AIndices, AData);
        }

        /// <inheritDoc />
        public new TData this[int ARow, int ACol]
        {
            [Pure]
            get { return this.Get(new MatrixIndices(ARow, ACol)); }
            set { this.Set(new MatrixIndices(ARow, ACol), value); }
        }
        #endregion
    
        #region IAssignable<MatrixBase<TData>>
        /// <inheritDoc />
        public void Assign(MatrixBase<TData> AOther)
        {
            if (AOther == null)
                throw new ArgumentNullException("AOther");

            if (this.Size != AOther.Size)
                throw new ArgumentException("Matrix dimensions do not match.");

            for (int M = 0; M < this.Size.M; M++)
                for (int N = 0; N < this.Size.N; N++)
                {
                    MatrixIndices miIndices = new MatrixIndices(M, N);
                    this.DirectSet(miIndices, AOther.DirectGet(miIndices));
                }
        }
        #endregion
    }
}