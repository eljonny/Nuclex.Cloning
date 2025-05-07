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
namespace Nuclex.Cloning.Tests
{
    using System.Collections.Generic;
    using Interfaces;
    using NUnit.Framework;
    using NUnit.Framework.Legacy;

    /// <summary>Unit Test for the reflection-based cloner</summary>
    [TestFixture]
    internal class ReflectionClonerTest : CloneFactoryTest
    {
        /// <summary>Initializes a new unit test suite for the reflection cloner</summary>
        public ReflectionClonerTest()
        {
            this.cloneFactory = new ReflectionCloner();
        }

        /// <summary>Verifies that cloning a null object simply returns null</summary>
        [Test]
        public void CloningNullYieldsNull()
        {
            Assert.That(this.cloneFactory.DeepFieldClone<object>(null), Is.Null);
            Assert.That(this.cloneFactory.DeepPropertyClone<object>(null), Is.Null);
            Assert.That(this.cloneFactory.ShallowFieldClone<object>(null), Is.Null);
            Assert.That(this.cloneFactory.ShallowPropertyClone<object>(null), Is.Null);
        }

        /// <summary>
        ///     Verifies that clones of objects whose class doesn't possess a default constructor
        ///     can be made
        /// </summary>
        [Test]
        public void ClassWithoutDefaultConstructorCanBeCloned()
        {
            var original = new ClassWithoutDefaultConstructor(1234);
            ClassWithoutDefaultConstructor clone = this.cloneFactory.DeepFieldClone(original);

            Assert.That(clone, Is.Not.SameAs(original));
            Assert.That(clone.Dummy, Is.EqualTo(original.Dummy));
        }

        /// <summary>Verifies that clones of primitive types can be created</summary>
        [Test]
        public void PrimitiveTypesCanBeCloned()
        {
            int original = 12345;
            int clone = this.cloneFactory.ShallowFieldClone(original);
            Assert.That(clone, Is.EqualTo(original));
        }

        /// <summary>Verifies that shallow clones of arrays can be made</summary>
        [Test]
        public void ShallowClonesOfArraysCanBeMade()
        {
            var original = new[]
            {
                new TestReferenceType {TestField = 123, TestProperty = 456}
            };
            TestReferenceType[] clone = this.cloneFactory.ShallowFieldClone(original);

            Assert.That(clone[0], Is.SameAs(original[0]));
        }

        /// <summary>Verifies that deep clones of arrays can be made</summary>
        [Test]
        public void DeepClonesOfArraysCanBeMade()
        {
            var original = new[]
            {
                new TestReferenceType {TestField = 123, TestProperty = 456}
            };
            TestReferenceType[] clone = this.cloneFactory.DeepFieldClone(original);

            Assert.That(clone[0], Is.Not.SameAs(original[0]));
            Assert.That(clone[0].TestField, Is.EqualTo(original[0].TestField));
            Assert.That(clone[0].TestProperty, Is.EqualTo(original[0].TestProperty));
        }

        /// <summary>Verifies that deep clones of a generic list can be made</summary>
        [Test]
        public void GenericListsCanBeCloned()
        {
            var original = new List<int>(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
            List<int> clone = this.cloneFactory.DeepFieldClone(original);

            CollectionAssert.AreEqual(clone, original);
        }

        /// <summary>Verifies that deep clones of a generic dictionary can be made</summary>
        [Test]
        public void GenericDictionariesCanBeCloned()
        {
            var original = new Dictionary<int, string>();
            original.Add(1, "one");
            Dictionary<int, string> clone = this.cloneFactory.DeepFieldClone(original);

            Assert.That(clone[1], Is.EqualTo("one"));
        }

        /// <summary>
        ///     Verifies that a field-based shallow clone of a value type can be performed
        /// </summary>
        [Test]
        public void ShallowFieldBasedClonesOfValueTypesCanBeMade()
        {
            HierarchicalValueType original = CreateValueType();
            HierarchicalValueType clone = this.cloneFactory.ShallowFieldClone(original);
            VerifyClone(ref original, ref clone, false, false);
        }

        /// <summary>
        ///     Verifies that a field-based shallow clone of a reference type can be performed
        /// </summary>
        [Test]
        public void ShallowFieldBasedClonesOfReferenceTypesCanBeMade()
        {
            HierarchicalReferenceType original = CreateReferenceType();
            HierarchicalReferenceType clone = this.cloneFactory.ShallowFieldClone(original);
            VerifyClone(original, clone, false, false);
        }

        /// <summary>
        ///     Verifies that a field-based deep clone of a value type can be performed
        /// </summary>
        [Test]
        public void DeepFieldBasedClonesOfValueTypesCanBeMade()
        {
            HierarchicalValueType original = CreateValueType();
            HierarchicalValueType clone = this.cloneFactory.DeepFieldClone(original);
            VerifyClone(ref original, ref clone, true, false);
        }

        /// <summary>
        ///     Verifies that a field-based deep clone of a reference type can be performed
        /// </summary>
        [Test]
        public void DeepFieldBasedClonesOfReferenceTypesCanBeMade()
        {
            HierarchicalReferenceType original = CreateReferenceType();
            HierarchicalReferenceType clone = this.cloneFactory.DeepFieldClone(original);
            VerifyClone(original, clone, true, false);
        }

        /// <summary>
        ///     Verifies that a property-based shallow clone of a value type can be performed
        /// </summary>
        [Test]
        public void ShallowPropertyBasedClonesOfValueTypesCanBeMade()
        {
            HierarchicalValueType original = CreateValueType();
            HierarchicalValueType clone = this.cloneFactory.ShallowPropertyClone(original);
            VerifyClone(ref original, ref clone, false, true);
        }

        /// <summary>
        ///     Verifies that a property-based shallow clone of a reference type can be performed
        /// </summary>
        [Test]
        public void ShallowPropertyBasedClonesOfReferenceTypesCanBeMade()
        {
            HierarchicalReferenceType original = CreateReferenceType();
            HierarchicalReferenceType clone = this.cloneFactory.ShallowPropertyClone(original);
            VerifyClone(original, clone, false, true);
        }

        /// <summary>
        ///     Verifies that a property-based deep clone of a value type can be performed
        /// </summary>
        [Test]
        public void DeepPropertyBasedClonesOfValueTypesCanBeMade()
        {
            HierarchicalValueType original = CreateValueType();
            HierarchicalValueType clone = this.cloneFactory.DeepPropertyClone(original);
            VerifyClone(ref original, ref clone, true, true);
        }

        /// <summary>
        ///     Verifies that a property-based deep clone of a reference type can be performed
        /// </summary>
        [Test]
        public void DeepPropertyBasedClonesOfReferenceTypesCanBeMade()
        {
            HierarchicalReferenceType original = CreateReferenceType();
            HierarchicalReferenceType clone = this.cloneFactory.DeepPropertyClone(original);
            VerifyClone(original, clone, true, true);
        }

        /// <summary>Clone factory being tested</summary>
        private readonly ICloneFactory cloneFactory;
    }
}