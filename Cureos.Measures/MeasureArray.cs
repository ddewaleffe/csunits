// Copyright (c) 2011 Anders Gustafsson, Cureos AB.
// All rights reserved. This software and the accompanying materials
// are made available under the terms of the Eclipse Public License v1.0
// which accompanies this distribution, and is available at
// http://www.eclipse.org/legal/epl-v10.html

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cureos.Measures.Extensions;

#if SINGLE
using AmountType = System.Single;
#elif DECIMAL
using AmountType = System.Decimal;
#elif DOUBLE
using AmountType = System.Double;
#endif

namespace Cureos.Measures
{
    public class MeasureArray<Q> : IEnumerable<AmountType>, IMeasureArray where Q : struct, IQuantity
    {
        #region MEMBER VARIABLES

        private readonly AmountType[] mAmounts;
        private readonly Unit mUnit;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the reference unit</param>
        public MeasureArray(IEnumerable<double> iAmounts)
        {
#if DOUBLE
            mAmounts = iAmounts.ToArray();
#elif SINGLE
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#elif DECIMAL
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the reference unit</param>
        public MeasureArray(IEnumerable<float> iAmounts)
        {
#if DOUBLE
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#elif SINGLE
            mAmounts = iAmounts.ToArray();
#elif DECIMAL
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the reference unit</param>
        public MeasureArray(IEnumerable<decimal> iAmounts)
        {
#if DOUBLE
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#elif SINGLE
            mAmounts = iAmounts.Select(a => (AmountType)a).ToArray();
#elif DECIMAL
            mAmounts = iAmounts.ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the <paramref name="iUnit">specified unit</paramref></param>
        /// <param name="iUnit">Unit in which the amount array is originally specified</param>
        public MeasureArray(IEnumerable<double> iAmounts, Unit iUnit)
        {
            AssertValidUnit(iUnit);
#if DOUBLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit(a)).ToArray();
#elif SINGLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#elif DECIMAL
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the <paramref name="iUnit">specified unit</paramref></param>
        /// <param name="iUnit">Unit in which the amount array is originally specified</param>
        public MeasureArray(IEnumerable<float> iAmounts, Unit iUnit)
        {
            AssertValidUnit(iUnit);
#if DOUBLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#elif SINGLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit(a)).ToArray();
#elif DECIMAL
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        /// <summary>
        /// Initializes an array of amounts in the reference unit of the IQuantity type
        /// </summary>
        /// <param name="iAmounts">Array of amounts, given in the <paramref name="iUnit">specified unit</paramref></param>
        /// <param name="iUnit">Unit in which the amount array is originally specified</param>
        public MeasureArray(IEnumerable<decimal> iAmounts, Unit iUnit)
        {
            AssertValidUnit(iUnit);
#if DOUBLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#elif SINGLE
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit((AmountType)a)).ToArray();
#elif DECIMAL
            mAmounts = iAmounts.Select(a => iUnit.ConvertAmountToReferenceUnit(a)).ToArray();
#endif
            mUnit = Quantity<Q>.ReferenceUnit;
        }

        #endregion
        
        #region Implementation of IMeasureArray

        /// <summary>
        /// Gets the array of measured amounts in the <see cref="IMeasureArray.Unit">current unit of measure</see>
        /// </summary>
        public AmountType[] Amounts
        {
            get { return mAmounts; }
        }

        /// <summary>
        /// Gets the unit of measure
        /// </summary>
        public Unit Unit
        {
            get { return mUnit; }
        }

        /// <summary>
        /// Gets the collection of measured amounts in the <paramref name="iUnit">specified unit</paramref>
        /// </summary>
        /// <param name="iUnit">Unit in which the array of measured amounts should be returned</param>
        /// <returns>Collection of measured amounts, given in the <paramref name="iUnit">specified unit</paramref></returns>
        /// <exception cref="InvalidOperationException">if the specified unit is not of the same quantity as the measure</exception>
        public IEnumerable<AmountType> GetAmounts(Unit iUnit)
        {
            if (Quantity<Q>.IsQuantityOf(iUnit))
            {
                return mAmounts.Select(a => iUnit.ConvertAmountFromReferenceUnit(a));
            }
            throw new InvalidOperationException(
                String.Format("Quantity of unit {0} is not equal to measured quantity {1}",
                iUnit, Quantity<Q>.Value));
        }

        #endregion

        #region Implementation of IEnumerable

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<AmountType> GetEnumerator()
        {
            return mAmounts.Cast<AmountType>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets the <paramref name="i">i:th</paramref> measure component of the measure array
        /// </summary>
        /// <param name="i">Zero-based index of the measure array</param>
        /// <returns>The <paramref name="i">i:th</paramref> component of the measure array</returns>
        public Measure<Q> this[uint i]
        {
            get { return new Measure<Q>(mAmounts[i]); }
        }

        #endregion

        #region METHODS

        private static void AssertValidUnit(Unit iUnit)
        {
            if (!Quantity<Q>.IsQuantityOf(iUnit))
                throw new InvalidOperationException(String.Format("Unit {0} is not of quantity {1}", iUnit, Quantity<Q>.Value));
        }

        #endregion
    }
}