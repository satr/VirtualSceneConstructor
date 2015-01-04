using NUnit.Framework;
using VirtualScene.Entities.SceneEntities;
using VirtualScene.Entities.SceneEntities.CalculationStrategies;
using VirtualScene.Entities.SceneEntities.SceneElements;

namespace VirtualScene.BusinessComponents.TestSuite
{
    [TestFixture]
    class NumberOfTeethAndOutsideDiameterTestCases
    {
        [TestCase(10f, 10, 
                  8.33f, 1.31f, 0.83f, 1.67f, 1.80f, 2.62f
                  )]
        [TestCase(10f, 20, 
                  9.09f, 0.71f, 0.45f, 0.91f, 1.00f, 1.43f)]
        [TestCase(10f, 30, 
                  9.38f, 0.49f, 0.31f, 0.63f, 0.69f, 0.98f)]
        /*outsideDiameter, numberOfTeeth
          pitchDiameter, toothThickness, addendum, workingDepth, wholeDepth, circularPitch
          baseCircularPitchAngle, toothThicknessAngle*/
        public void TestCalculateParams(params object[] args)
        {
            var outsideDiameter = (float)args[0];
            var numberOfTeeth = (int)args[1];

            var spurGearEntity = new SpurGearEntity
            {
                CalculationStrategy = new SpurGearCalculationStrategyByNumberOfTeethAndOutsideDiameter()
            };
            var spurGear = SpurGear.Create(1f, 1f, 1f, numberOfTeeth, 0f);
            spurGearEntity.Geometry = spurGear;

            spurGearEntity.OutsideDiameter = outsideDiameter;

            var pitchDiameter = (float) args[2];
            var toothThickness  = (float) args[3];
            var addendum  = (float) args[4];
            var workingDepth  = (float) args[5];
            var wholeDepth  = (float) args[6];
            var circularPitch = (float)args[7];
            Assert.AreEqual(pitchDiameter, spurGear.PitchDiameter, 0.01);
            Assert.AreEqual(toothThickness, spurGear.ToothThickness, 0.01);
            Assert.AreEqual(addendum, spurGear.Addendum, 0.01);
            Assert.AreEqual(workingDepth, spurGear.WorkingDepth, 0.01);
            Assert.AreEqual(wholeDepth, spurGear.WholeDepth, 0.01);
            Assert.AreEqual(circularPitch, spurGear.CircularPitch, 0.01);
        }
    }
}
