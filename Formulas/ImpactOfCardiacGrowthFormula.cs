namespace DICOMReporting.Formulas {
	public static class ImpactOfCardiacGrowthFormula {

		public static double GetZScore(double measurement, Constants constants) {
			return (measurement - constants.Average) / constants.SD;
		}
		public class Constants {
			public static Constants MitralValveEWave(double Age) {
				double[] averages = { 79.7, 95.2, 94.4, 94.5, 90.3 };
				double[] sds = { 18.8, 19.5, 14.8, 16, 17.8 };
				return new Constants(averages, sds, Age);
			}
			public static Constants MitralValveAWave(double Age) {
				double[] averages = { 65.3, 61.3, 49.4, 49.5, 45.5 };
				double[] sds = { 13.3, 12.1, 12.5, 13.8, 13.2 };
				return new Constants(averages, sds, Age);
			}
			public static Constants MitralEA_Ratio(double Age) {
				double[] averages = { 1.24, 1.6, 1.99, 2.02, 2.13 };
				double[] sds = { 0.3, 0.46, 0.51, 0.58, 0.65 };
				return new Constants(averages, sds, Age);
			}
			public static Constants LVMassIndex(double Age) {
				double[] averages = { 18.9, 43.6, 82.3, 110.1, 158.4 };
				double[] sds = { 6.5, 16.4, 28.3, 32.9, 48.5 };
				return new Constants(averages, sds, Age);
			}
			public static Constants HeartRate(double Age) {
				double[] averages = { 124, 105, 80, 75, 69 };
				double[] sds = { 16, 17, 11, 12, 16 };
				return new Constants(averages, sds, Age);
			}
			public static Constants PulmVeinSWave(double Age) {
				double[] averages = { 44.6, 48, 50.7, 49, 47.7 };
				double[] sds = { 10.3, 8.9, 11.3, 11.1, 7.3 };
				return new Constants(averages, sds, Age);
			}
			public static Constants PulmVeinDWave(double Age) {
				double[] averages = { 46, 54.5, 53.3, 58.4, 57.9 };
				double[] sds = { 9.5, 11, 11.4, 12.1, 15 };
				return new Constants(averages, sds, Age);
			}
			public static Constants PulmVeinAWave(double Age) {
				double[] averages = { 16.4, 20.6, 20.2, 21.2, 20 };
				double[] sds = { 6.3, 4.3, 3.8, 4.9, 5.2 };
				return new Constants(averages, sds, Age);
			}
			public static Constants FractionalShortening(double Age) {
				double[] averages = { 38.9, 38, 37.4, 37.4, 36.4 };
				double[] sds = { 4.1, 3.6, 3.8, 4.2, 4.3 };
				return new Constants(averages, sds, Age);
			}
			public static Constants MitralAnnulusE(double Age) {
				double[] averages = { 9.7, 15.1, 17.2, 19.6, 20.6 };
				double[] sds = { 3.3, 3.4, 3.7, 3.4, 3.8 };
				return new Constants(averages, sds, Age);
			}
			public static Constants MitralAnnulusA(double Age) {
				double[] averages = { 5.7, 6.5, 6.7, 6.4, 6.7 };
				double[] sds = { 1.8, 1.9, 1.9, 1.8, 1.6 };
				return new Constants(averages, sds, Age);
			}
			public static Constants MitralAnnulusS(double Age) {
				double[] averages = { 5.7, 7.7, 9.5, 10.8, 12.3 };
				double[] sds = { 1.6, 2.1, 2.1, 2.9, 2.9 };
				return new Constants(averages, sds, Age);
			}
			public static Constants SeptalAnnulusE(double Age) {
				double[] averages = { 8.1 ,  11.8  ,  13.4  ,  14.5  ,  14.9 };
				double[] sds = { 2.5, 1.3, 1.9, 2.6, 2.4 };
				return new Constants(averages, sds, Age);
			}
			public static Constants SeptalAnnulusA(double Age) {
				double[] averages = { 6.1 ,  6  , 5.9, 6.1, 6.2 };
				double[] sds = { 1.5 ,   1.3, 1.3, 2.3, 1.5 };
				return new Constants(averages, sds, Age);
			}
			public static Constants SeptalAnnulusS(double Age) {
				double[] averages = { 5.4  , 7.1, 8 ,  8.2, 9 };
				double[] sds = { 1.2  ,  1.5, 1.3 ,1.3, 1.5 };
				return new Constants(averages, sds, Age);
			}
			public static Constants TricuspidAnnulusE(double Age) {
				double[] averages = { 13.8 , 17.1  ,  16.5  ,  16.5 ,   16.7 };
				double[] sds = { 8.2 ,   4  , 3  , 3.1, 2.8 };
				return new Constants(averages, sds, Age);
			}
			public static Constants TricuspidAnnulusA(double Age) {
				double[] averages = { 9.8  , 10.9 ,   9.8 ,10.3,    10.1 };
				double[] sds = { 2.4  ,  2.7, 2.7, 3.4, 2.6 };
				return new Constants(averages, sds, Age);
			}
			public static Constants TricuspidAnnulusS(double Age) {
				double[] averages = { 10.2 , 13.2  ,  13.4 ,   13.9  ,  14.2 };
				double[] sds = { 5.5  ,  2 ,  2 ,  2.4, 2.3 };
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
				if (age < 1) {
					bracket = 0;
				}
				else if (age <= 5) {
					bracket = 1;
				}
				else if (age <= 9) {
					bracket = 2;
				}
				else if (age <= 13) {
					bracket = 3;
				}
				else {
					bracket = 4;
				}
				Average = averages[bracket];
				SD = sds[bracket];
			}
		}
	}
}
