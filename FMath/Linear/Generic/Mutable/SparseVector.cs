using System.Collections.Generic;
using System.Linq;

using FMath.Linear.Generic.Base;
using FMath.Linear.Static;

namespace FMath.Linear.Generic.Mutable
{
    /// <summary>
    /// Mutable vector that is implemented using a lookup.
    /// </summary>
    /// <typeparam name="TData">The type of the stored data.</typeparam>
    /// <seealso cref="MutableVectorBase{TData}" />
    public class SparseVector<TData> :
        MutableVectorBase<TData>
    {
        private readonly TData FDefault;
        private readonly IEqualityComparer<TData> FComparer;

        protected readonly SortedList<int, TData> FElements;

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseVector{TData}"/> class.
        /// </summary>
        /// <param name="ASize">The size of the vector.</param>
        /// <param name="ADefault">The default element.</param>
        /// <param name="AComparer">The element comparer.</param>
        /// <param name="AElements">An optional initializer for the elements.</param>
        public SparseVector(
            int ASize,
            TData ADefault = default(TData),
            IEqualityComparer<TData> AComparer = null,
            IDictionary<int, TData> AElements = null)
            : base(ASize)
        {
            if (AComparer == null)
                AComparer = EqualityComparer<TData>.Default;

            this.FDefault = ADefault;
            this.FComparer = AComparer;

            this.FElements = new SortedList<int, TData>();

            if (AElements != null)
                foreach (KeyValuePair<int, TData> kvpPair in AElements
                    .Where(APair => !AComparer.Equals(APair.Value, ADefault) && this.IsDefined(APair.Key)))
                    this.FElements.Add(kvpPair.Key, kvpPair.Value);
        }

        protected internal override TData DirectGet(int AIndex)
        {
            TData tOut;
            if (!this.FElements.TryGetValue(AIndex, out tOut))
                return this.FDefault;

            return tOut;
        }
        protected internal override void DirectSet(int AIndex, TData AData)
        {
            if (this.Comparer.Equals(AData, this.FDefault))
                this.FElements.Remove(AIndex);
            else
                this.FElements[AIndex] = AData;
        }

        /// <inheritDoc />
        public override object Clone()
        {
            return new SparseVector<TData>(this.Size, this.Default, this.Comparer, this.FElements);
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