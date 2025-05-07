#region CPL License
/*
Nuclex Framework
Copyright (C) 2002-2012 Nuclex Development Labs

This library is free software; you can redistribute it and/or
modify it under the terms of the IBM Common Public License as
published by the IBM Corporation; either version 1.0 of the
License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
IBM Common Public License for more details.

You should have received a copy of the IBM Common Public
License along with this library
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuclex.Cloning.Tests
{
    using NUnit.Framework;

    /// <summary>Base class for unit tests verifying the clone factory</summary>
    internal abstract class CloneFactoryTest
    {

        #region class DerivedReferenceType

        /// <summary>A derived reference type being used for testing</summary>
        protected class DerivedReferenceType : TestReferenceType
        {

            /// <summary>Field holding an integer value for testing</summary>
            public int DerivedField;
            /// <summary>Property holding an integer value for testing</summary>
            public int DerivedProperty { get; set; }

        }

        #endregion // class DerivedReferenceType

        #region class TestReferenceType

        /// <summary>A reference type being used for testing</summary>
        protected class TestReferenceType
        {

            /// <summary>Field holding an integer value for testing</summary>
            public int TestField;
            /// <summary>Property holding an integer value for testing</summary>
            public int TestProperty { get; set; }

        }

        #endregion // class TestReferenceType

        #region struct TestValueType

        /// <summary>A value type being used for testing</summary>
        protected struct TestValueType
        {

            /// <summary>Field holding an integer value for testing</summary>
            public int TestField;
            /// <summary>Property holding an integer value for testing</summary>
            public int TestProperty { get; set; }

        }

        #endregion // struct TestValueType

        #region struct HierarchicalValueType

        /// <summary>A value type containiner other complex types used for testing</summary>
        protected struct HierarchicalValueType
        {

            /// <summary>Field holding an integer value for testing</summary>
            public int TestField;
            /// <summary>Property holding an integer value for testing</summary>
            public int TestProperty { get; set; }
            /// <summary>Value type field for testing</summary>
            public TestValueType ValueTypeField;
            /// <summary>Value type property for testing</summary>
            public TestValueType ValueTypeProperty { get; set; }
            /// <summary>Reference type field for testing</summary>
            public TestReferenceType ReferenceTypeField;
            /// <summary>Reference type property for testing</summary>
            public TestReferenceType ReferenceTypeProperty { get; set; }
            /// <summary>An array field of reference types</summary>
            public TestReferenceType[,][] ReferenceTypeArrayField;
            /// <summary>An array property of reference types</summary>
            public TestReferenceType[,][] ReferenceTypeArrayProperty { get; set; }
            /// <summary>A reference type field that's always null</summary>
            public TestReferenceType AlwaysNullField;
            /// <summary>A reference type property that's always null</summary>
            public TestReferenceType AlwaysNullProperty { get; set; }
            /// <summary>A property that only has a getter</summary>
            public TestReferenceType GetOnlyProperty { get { return null; } }
            /// <summary>A property that only has a setter</summary>
            public TestReferenceType SetOnlyProperty { set { } }
            /// <summary>Field typed as base class holding a derived instance</summary>
            public TestReferenceType DerivedField;

            public HierarchicalValueType()
            {
                AlwaysNullField = null;
            }

            /// <summary>Field typed as base class holding a derived instance</summary>
            public TestReferenceType DerivedProperty { get; set; }

        }

        #endregion // struct HierarchicalValueType

        #region struct HierarchicalReferenceType

        /// <summary>A value type containiner other complex types used for testing</summary>
        protected class HierarchicalReferenceType
        {

            /// <summary>Field holding an integer value for testing</summary>
            public int TestField;
            /// <summary>Property holding an integer value for testing</summary>
            public int TestProperty { get; set; }
            /// <summary>Value type field for testing</summary>
            public TestValueType ValueTypeField;
            /// <summary>Value type property for testing</summary>
            public TestValueType ValueTypeProperty { get; set; }
            /// <summary>Reference type field for testing</summary>
            public TestReferenceType ReferenceTypeField;
            /// <summary>Reference type property for testing</summary>
            public TestReferenceType ReferenceTypeProperty { get; set; }
            /// <summary>An array field of reference types</summary>
            public TestReferenceType[,][] ReferenceTypeArrayField;
            /// <summary>An array property of reference types</summary>
            public TestReferenceType[,][] ReferenceTypeArrayProperty { get; set; }
            /// <summary>A reference type field that's always null</summary>
            public TestReferenceType AlwaysNullField;
            /// <summary>A reference type property that's always null</summary>
            public TestReferenceType AlwaysNullProperty { get; set; }
            /// <summary>A property that only has a getter</summary>
            public TestReferenceType GetOnlyProperty { get { return null; } }
            /// <summary>A property that only has a s</summary>
            public TestReferenceType SetOnlyProperty { set { } }
            /// <summary>Field typed as base class holding a derived instance</summary>
            public TestReferenceType DerivedField;
            /// <summary>Field typed as base class holding a derived instance</summary>
            public TestReferenceType DerivedProperty { get; set; }

            public HierarchicalReferenceType()
            {
                AlwaysNullField = null;
            }

        }

        #endregion // struct HierarchicalReferenceType

        #region class ClassWithoutDefaultConstructor

        /// <summary>A class that does not have a default constructor</summary>
        public class ClassWithoutDefaultConstructor
        {

            /// <summary>
            ///   Initializes a new instance of the class without default constructor
            /// </summary>
            /// <param name="dummy">Dummy value that will be saved by the instance</param>
            public ClassWithoutDefaultConstructor(int dummy)
            {
                this.dummy = dummy;
            }

            /// <summary>Dummy value that has been saved by the instance</summary>
            public int Dummy
            {
                get { return this.dummy; }
            }

            /// <summary>Dummy value that has been saved by the instance</summary>
            private int dummy;

        }

        #endregion // class ClassWithoutDefaultConstructor

        /// <summary>
        ///   Verifies that a cloned object exhibits the expected state for the type of
        ///   clone that has been performed
        /// </summary>
        /// <param name="original">Original instance the clone was created from</param>
        /// <param name="clone">Cloned instance that will be checked for correctness</param>
        /// <param name="isDeepClone">Whether the cloned instance is a deep clone</param>
        /// <param name="isPropertyBasedClone">
        ///   Whether a property-based clone was performed
        /// </param>
        protected static void VerifyClone(
          HierarchicalReferenceType original, HierarchicalReferenceType clone,
          bool isDeepClone, bool isPropertyBasedClone
        )
        {
            Assert.That(clone, Is.Not.SameAs(original));
            if (isPropertyBasedClone)
            {
                Assert.That(clone.TestField, Is.EqualTo(0));
                Assert.That(clone.ValueTypeField.TestField, Is.EqualTo(0));
                Assert.That(clone.ValueTypeField.TestProperty, Is.EqualTo(0));
                Assert.That(clone.ValueTypeProperty.TestField, Is.EqualTo(0));
                Assert.That(clone.ReferenceTypeField, Is.Null);
                Assert.That(clone.DerivedField, Is.Null);

                if (isDeepClone)
                {
                    Assert.That(clone.ReferenceTypeProperty, Is.Not.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.Not.SameAs(original.ReferenceTypeArrayProperty));
                    Assert.That(clone.DerivedProperty, Is.Not.SameAs(original.DerivedProperty));
                    Assert.That(clone.DerivedProperty, Is.InstanceOf<DerivedReferenceType>());

                    var originalDerived = (DerivedReferenceType)original.DerivedProperty;
                    var clonedDerived = (DerivedReferenceType)clone.DerivedProperty;
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    Assert.That(clone.ReferenceTypeProperty.TestField, Is.EqualTo(0));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][0]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][2]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0].TestField, Is.EqualTo(0));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2].TestField, Is.EqualTo(0));
                }
                else
                {
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.SameAs(original.ReferenceTypeArrayProperty));
                }
            }
            else
            {
                Assert.That(clone.TestField, Is.EqualTo(original.TestField));
                Assert.That(clone.ValueTypeField.TestField, Is.EqualTo(original.ValueTypeField.TestField));
                Assert.That(clone.ValueTypeField.TestProperty, Is.EqualTo(original.ValueTypeField.TestProperty));
                Assert.That(clone.ValueTypeProperty.TestField, Is.EqualTo(original.ValueTypeProperty.TestField));
                Assert.That(clone.ReferenceTypeField.TestField, Is.EqualTo(original.ReferenceTypeField.TestField));
                Assert.That(clone.ReferenceTypeField.TestProperty, Is.EqualTo(original.ReferenceTypeField.TestProperty));
                Assert.That(clone.ReferenceTypeProperty.TestField, Is.EqualTo(original.ReferenceTypeProperty.TestField));

                if (isDeepClone)
                {
                    Assert.That(clone.ReferenceTypeField, Is.Not.SameAs(original.ReferenceTypeField));
                    Assert.That(clone.ReferenceTypeProperty, Is.Not.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.DerivedField, Is.Not.SameAs(original.DerivedField));
                    Assert.That(clone.DerivedProperty, Is.Not.SameAs(original.DerivedProperty));
                    Assert.That(clone.DerivedField, Is.InstanceOf<DerivedReferenceType>());
                    Assert.That(clone.DerivedProperty, Is.InstanceOf<DerivedReferenceType>());

                    var originalDerived = (DerivedReferenceType)original.DerivedField;
                    var clonedDerived = (DerivedReferenceType)clone.DerivedField;
                    Assert.That(clonedDerived.TestField, Is.EqualTo(originalDerived.TestField));
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedField, Is.EqualTo(originalDerived.DerivedField));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    originalDerived = (DerivedReferenceType)original.DerivedProperty;
                    clonedDerived = (DerivedReferenceType)clone.DerivedProperty;
                    Assert.That(clonedDerived.TestField, Is.EqualTo(originalDerived.TestField));
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedField, Is.EqualTo(originalDerived.DerivedField));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    Assert.That(clone.ReferenceTypeArrayField, Is.Not.SameAs(original.ReferenceTypeArrayField));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.Not.SameAs(original.ReferenceTypeArrayProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][0]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][2]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0].TestField, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][0].TestField));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2].TestField, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][2].TestField));
                }
                else
                {
                    Assert.That(clone.DerivedField, Is.SameAs(original.DerivedField));
                    Assert.That(clone.DerivedProperty, Is.SameAs(original.DerivedProperty));
                    Assert.That(clone.ReferenceTypeField, Is.SameAs(original.ReferenceTypeField));
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayField, Is.SameAs(original.ReferenceTypeArrayField));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.SameAs(original.ReferenceTypeArrayProperty));
                }
            }
        }

        /// <summary>
        ///   Verifies that a cloned object exhibits the expected state for the type of
        ///   clone that has been performed
        /// </summary>
        /// <param name="original">Original instance the clone was created from</param>
        /// <param name="clone">Cloned instance that will be checked for correctness</param>
        /// <param name="isDeepClone">Whether the cloned instance is a deep clone</param>
        /// <param name="isPropertyBasedClone">
        ///   Whether a property-based clone was performed
        /// </param>
        protected static void VerifyClone(
          ref HierarchicalValueType original, ref HierarchicalValueType clone,
          bool isDeepClone, bool isPropertyBasedClone
        )
        {
            if (isPropertyBasedClone)
            {
                Assert.That(clone.TestField, Is.EqualTo(0));
                Assert.That(clone.ValueTypeField.TestField, Is.EqualTo(0));
                Assert.That(clone.ValueTypeField.TestProperty, Is.EqualTo(0));
                Assert.That(clone.ValueTypeProperty.TestField, Is.EqualTo(0));
                Assert.That(clone.ReferenceTypeField, Is.Null);
                Assert.That(clone.DerivedField, Is.Null);

                if (isDeepClone)
                {
                    Assert.That(clone.ReferenceTypeProperty, Is.Not.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.Not.SameAs(original.ReferenceTypeArrayProperty));
                    Assert.That(clone.DerivedProperty, Is.Not.SameAs(original.DerivedProperty));
                    Assert.That(clone.DerivedProperty, Is.InstanceOf<DerivedReferenceType>());

                    var originalDerived = (DerivedReferenceType)original.DerivedProperty;
                    var clonedDerived = (DerivedReferenceType)clone.DerivedProperty;
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    Assert.That(clone.ReferenceTypeProperty.TestField, Is.EqualTo(0));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][0]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][2]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0].TestField, Is.EqualTo(0));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2].TestField, Is.EqualTo(0));
                }
                else
                {
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.SameAs(original.ReferenceTypeArrayProperty));
                }
            }
            else
            {
                Assert.That(clone.TestField, Is.EqualTo(original.TestField));
                Assert.That(clone.ValueTypeField.TestField, Is.EqualTo(original.ValueTypeField.TestField));
                Assert.That(clone.ValueTypeField.TestProperty, Is.EqualTo(original.ValueTypeField.TestProperty));
                Assert.That(clone.ValueTypeProperty.TestField, Is.EqualTo(original.ValueTypeProperty.TestField));
                Assert.That(clone.ReferenceTypeField.TestField, Is.EqualTo(original.ReferenceTypeField.TestField));
                Assert.That(clone.ReferenceTypeField.TestProperty, Is.EqualTo(original.ReferenceTypeField.TestProperty));
                Assert.That(clone.ReferenceTypeProperty.TestField, Is.EqualTo(original.ReferenceTypeProperty.TestField));

                if (isDeepClone)
                {
                    Assert.That(clone.ReferenceTypeField, Is.Not.SameAs(original.ReferenceTypeField));
                    Assert.That(clone.ReferenceTypeProperty, Is.Not.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.DerivedField, Is.Not.SameAs(original.DerivedField));
                    Assert.That(clone.DerivedProperty, Is.Not.SameAs(original.DerivedProperty));
                    Assert.That(clone.DerivedField, Is.InstanceOf<DerivedReferenceType>());
                    Assert.That(clone.DerivedProperty, Is.InstanceOf<DerivedReferenceType>());

                    var originalDerived = (DerivedReferenceType)original.DerivedField;
                    var clonedDerived = (DerivedReferenceType)clone.DerivedField;
                    Assert.That(clonedDerived.TestField, Is.EqualTo(originalDerived.TestField));
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedField, Is.EqualTo(originalDerived.DerivedField));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    originalDerived = (DerivedReferenceType)original.DerivedProperty;
                    clonedDerived = (DerivedReferenceType)clone.DerivedProperty;
                    Assert.That(clonedDerived.TestField, Is.EqualTo(originalDerived.TestField));
                    Assert.That(clonedDerived.TestProperty, Is.EqualTo(originalDerived.TestProperty));
                    Assert.That(clonedDerived.DerivedField, Is.EqualTo(originalDerived.DerivedField));
                    Assert.That(clonedDerived.DerivedProperty, Is.EqualTo(originalDerived.DerivedProperty));

                    Assert.That(clone.ReferenceTypeArrayField, Is.Not.SameAs(original.ReferenceTypeArrayField));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.Not.SameAs(original.ReferenceTypeArrayProperty));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][0]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2], Is.Not.SameAs(original.ReferenceTypeArrayProperty[1, 3][2]));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0].TestField, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][0].TestField));
                    Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2].TestField, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][2].TestField));
                }
                else
                {
                    Assert.That(clone.DerivedField, Is.SameAs(original.DerivedField));
                    Assert.That(clone.DerivedProperty, Is.SameAs(original.DerivedProperty));
                    Assert.That(clone.ReferenceTypeField, Is.SameAs(original.ReferenceTypeField));
                    Assert.That(clone.ReferenceTypeProperty, Is.SameAs(original.ReferenceTypeProperty));
                    Assert.That(clone.ReferenceTypeArrayField, Is.SameAs(original.ReferenceTypeArrayField));
                    Assert.That(clone.ReferenceTypeArrayProperty, Is.SameAs(original.ReferenceTypeArrayProperty));
                }
            }

            Assert.That(clone.TestProperty, Is.EqualTo(original.TestProperty));
            Assert.That(clone.ValueTypeProperty.TestProperty, Is.EqualTo(original.ValueTypeProperty.TestProperty));
            Assert.That(clone.ReferenceTypeProperty.TestProperty, Is.EqualTo(original.ReferenceTypeProperty.TestProperty));
            Assert.That(clone.ReferenceTypeArrayProperty[1, 3][0].TestProperty, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][0].TestProperty));
            Assert.That(clone.ReferenceTypeArrayProperty[1, 3][2].TestProperty, Is.EqualTo(original.ReferenceTypeArrayProperty[1, 3][2].TestProperty));
        }

        /// <summary>Creates a value type with random data for testing</summary>
        /// <returns>A new value type with random data</returns>
        protected static HierarchicalValueType CreateValueType()
        {
            return new HierarchicalValueType()
            {
                TestField = 123,
                TestProperty = 321,
                ReferenceTypeArrayField = new TestReferenceType[2, 4][] {
          {
            null, null, null, null
          },
          {
            null, null, null,
            new TestReferenceType[3] {
              new TestReferenceType() { TestField = 101, TestProperty = 202 },
              null,
              new TestReferenceType() { TestField = 909, TestProperty = 808 }
            }
          },
        },
                ReferenceTypeArrayProperty = new TestReferenceType[2, 4][] {
          {
            null, null, null, null
          },
          {
            null, null, null,
            new TestReferenceType[3] {
              new TestReferenceType() { TestField = 303, TestProperty = 404 },
              null,
              new TestReferenceType() { TestField = 707, TestProperty = 606 }
            }
          },
        },
                ValueTypeField = new TestValueType()
                {
                    TestField = 456,
                    TestProperty = 654
                },
                ValueTypeProperty = new TestValueType()
                {
                    TestField = 789,
                    TestProperty = 987,
                },
                ReferenceTypeField = new TestReferenceType()
                {
                    TestField = 135,
                    TestProperty = 531
                },
                ReferenceTypeProperty = new TestReferenceType()
                {
                    TestField = 246,
                    TestProperty = 642,
                },
                DerivedField = new DerivedReferenceType()
                {
                    DerivedField = 100,
                    DerivedProperty = 200,
                    TestField = 300,
                    TestProperty = 400
                },
                DerivedProperty = new DerivedReferenceType()
                {
                    DerivedField = 500,
                    DerivedProperty = 600,
                    TestField = 700,
                    TestProperty = 800
                }
            };
        }

        /// <summary>Creates a reference type with random data for testing</summary>
        /// <returns>A new reference type with random data</returns>
        protected static HierarchicalReferenceType CreateReferenceType()
        {
            return new HierarchicalReferenceType()
            {
                TestField = 123,
                TestProperty = 321,
                ReferenceTypeArrayField = new TestReferenceType[2, 4][] {
          {
            null, null, null, null
          },
          {
            null, null, null,
            new TestReferenceType[3] {
              new TestReferenceType() { TestField = 101, TestProperty = 202 },
              null,
              new TestReferenceType() { TestField = 909, TestProperty = 808 }
            }
          },
        },
                ReferenceTypeArrayProperty = new TestReferenceType[2, 4][] {
          {
            null, null, null, null
          },
          {
            null, null, null,
            new TestReferenceType[3] {
              new TestReferenceType() { TestField = 303, TestProperty = 404 },
              null,
              new TestReferenceType() { TestField = 707, TestProperty = 606 }
            }
          },
        },
                ValueTypeField = new TestValueType()
                {
                    TestField = 456,
                    TestProperty = 654
                },
                ValueTypeProperty = new TestValueType()
                {
                    TestField = 789,
                    TestProperty = 987,
                },
                ReferenceTypeField = new TestReferenceType()
                {
                    TestField = 135,
                    TestProperty = 531
                },
                ReferenceTypeProperty = new TestReferenceType()
                {
                    TestField = 246,
                    TestProperty = 642,
                },
                DerivedField = new DerivedReferenceType()
                {
                    DerivedField = 100,
                    DerivedProperty = 200,
                    TestField = 300,
                    TestProperty = 400
                },
                DerivedProperty = new DerivedReferenceType()
                {
                    DerivedField = 500,
                    DerivedProperty = 600,
                    TestField = 700,
                    TestProperty = 800
                }
            };
        }

    }
}
