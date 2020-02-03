using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScriptEdit
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptEditSimplePasses()
        {
        }

        [Test]
        public void CreatePeople()
        {
            CreateRyohei();
            CreateGirl();
            CreateOldMan();
        }

        private void CreateRyohei()
        {
            const string name = "Ryohei";
            PersonInfo ryohei = new PersonInfo(name, new DateTime(1997, 3, 10), PersonInfo.ESex.Male);
            Assert.AreEqual(name, ryohei.Name);
            Assert.IsTrue(DateTime.Today > ryohei.Birthday);
            Assert.IsTrue(12 >= ryohei.Birthday.Month);
            Assert.IsTrue(31 >= ryohei.Birthday.Day);
            Assert.AreEqual(PersonInfo.ESex.Male, ryohei.Sex);
        }

        private void CreateGirl()
        {
            const string name = "Girl";
            PersonInfo girl = new PersonInfo(name, new DateTime(2014, 12, 28), PersonInfo.ESex.Female);
            Assert.AreEqual(name, girl.Name);
            Assert.IsTrue(DateTime.Today > girl.Birthday);
            Assert.IsTrue(12 >= girl.Birthday.Month);
            Assert.IsTrue(31 >= girl.Birthday.Day);
            Assert.AreEqual(PersonInfo.ESex.Female, girl.Sex);
        }

        private void CreateOldMan()
        {
            const string name = "OldMan";
            PersonInfo man = new PersonInfo(name, new DateTime(1945, 8, 20), PersonInfo.ESex.Male);
            Assert.AreEqual(name, man.Name);
            Assert.IsTrue(DateTime.Today > man.Birthday);
            Assert.IsTrue(12 >= man.Birthday.Month);
            Assert.IsTrue(31 >= man.Birthday.Day);
            Assert.AreEqual(PersonInfo.ESex.Male, man.Sex);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptEditWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}