//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Poz1.LogicProver.Model.World;
//using System.Collections.Generic;

//namespace Poz1.LogicProver.Test
//{

//    //Examples of world unifications from section 8.2.7 
//    [TestClass]
//    public class WorldUnification
//    {
//        [TestMethod]
//        public void Example1a()
//        {
//            var baseWorld = new WorldSymbol("0");
//            var wi1 = new WorldIndex(new WorldSymbol("2", new WorldSymbol("1", baseWorld)));
//            var wi2 = new WorldIndex(new WorldSymbol("w", baseWorld));

//            var relation = new AccessibilityRelation(
//              new List<IRelationProperty>()
//              {
//                    new SerialProperty(),
//                    new ReflexiveProperty(),
//                    new TransitiveProperty()
//              },
//              new List<WorldIndex>() { wi1, wi2 }
//            );

//            Assert.IsNotNull(relation.WorldUnify(wi1, wi2));
//        }

//        [TestMethod]
//        public void Example1b()
//        {
//            var baseWorld = new WorldSymbol("0");
//            var wi1 = new WorldIndex(new WorldSymbol("2", new WorldSymbol("1", baseWorld)));
//            var wi2 = new WorldIndex(new WorldSymbol("w", baseWorld));

//            var relation = new AccessibilityRelation(
//              new List<IRelationProperty>()
//              {
//                    new SerialProperty(),
//                    new ReflexiveProperty()
//              },
//              new List<WorldIndex>() { wi1, wi2 }
//            );

//            var resu = relation.WorldUnify(wi1, wi2);
//            Assert.IsNull(resu);
//        }

//        [TestMethod]
//        public void Example2()
//        {
//            var baseWorld = new WorldSymbol("0");
//            var wi1 = new WorldIndex(new WorldSymbol("g(y)",baseWorld));
//            var wi2 = new WorldIndex(new WorldSymbol("w", baseWorld));

//            var relation = new AccessibilityRelation(
//              new List<IRelationProperty>()
//              {
//                    new SerialProperty()
//              },
//              new List<WorldIndex>() { wi1, wi2 }
//            );

//            var t = relation.WorldUnify(wi1, wi2);
//            //Can be cahnged to be euqal to g(y)/w
//            Assert.IsNotNull(t);
//        }

//        [TestMethod]
//        public void Example4a()
//        {
//            var baseWorld = new WorldSymbol("0");
//            var wi1 = new WorldIndex(new WorldSymbol("f(w)", new WorldSymbol("w", baseWorld)));
//            var wi2 = new WorldIndex(new WorldSymbol("w1", baseWorld));

//            var relation = new AccessibilityRelation(
//              new List<IRelationProperty>()
//              {
//                    new SerialProperty()
//              },
//              new List<WorldIndex>() { wi1, wi2 }
//            );

//            var t = relation.WorldUnify(wi1, wi2);
//            Assert.IsNotNull(t);
//        }

//        [TestMethod]
//        public void Example5()
//        {
//            var baseWorld = new WorldSymbol("0");
//            var wi1 = new WorldIndex(new WorldSymbol("w", baseWorld));
//            var wi2 = new WorldIndex(new WorldSymbol("v", new WorldSymbol("1", baseWorld)));

//            var relation = new AccessibilityRelation(
//              new List<IRelationProperty>()
//              {
//                    new SerialProperty()
//              },
//              new List<WorldIndex>() { wi1, wi2 }
//            );

//            var t = relation.WorldUnify(wi1, wi2);
//            Assert.IsNotNull(t);
//        }
//    }
//}
