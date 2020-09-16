namespace DICOMReporting.Formulas {
	public class EchoManualFormula : IFormula {
		private Constants constants;

		public EchoManualFormula(Constants constants) {
			this.constants = constants;
		}
		public double GetZScore(double measurement) {
			return (measurement - constants.Average) / constants.SD;
		}
		public struct Constants {
			public static Constants MVDecelTime(double Age) {
				double[] averages = { 145 ,  157 ,172 };
				double[] sds = { 18 ,19 , 22 };
				return new Constants(averages, sds, Age);
			}
			public static Constants LVIVRT(double Age) {
				double[] averages = { 62 ,67  ,74 };
				double[] sds = { 10 ,10  ,13 };
				return new Constants(averages, sds, Age);
			}
			public double Average {
				get; private set;
			}
			public double SD {
				get; private set;
			}
			private Constants(double[] averages, double[] sds, double age) {
				int bracket;
				if (age < 8) {
					bracket = 0;
				}
				else if (age <= 12) {
					bracket = 1;
				}
				else {
					bracket = 2;
				}
				Average = averages[bracket];
				SD = sds[bracket];
			}
		}
	}
}
