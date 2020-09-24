using DICOMReporting.Data;
using System;

namespace DICOMReporting.Formulas {
	public class DilationOfAscendingAortaFormula : IFormula {
		private Constants constants;


		public double GetZScore(double observed_y) {
			double mean_y = (constants.Multiplier * Math.Log(constants.BSA)) + constants.Intercept;
			return (Math.Log(observed_y) - mean_y) / constants.MSE;
		}
		public bool ZScoreable() => true;
		public static DilationOfAscendingAortaFormula AscendingAorta(PatientData pd) {
			return new DilationOfAscendingAortaFormula(new Constants(0.421, 2.898, 0.09111, pd));
		}/*
			public static Constants AorticAnnulus(double BSA) {
				return new Constants(0.426, 2.732, 0.10392, BSA);
			}
			public static Constants AorticSinuses(double BSA) {
				return new Constants(0.443, 3.021, 0.10173, BSA);
			}
			public static Constants STJunction(double BSA) {
				return new Constants(0.434, 2.819, 0.10961, BSA);
			}*/
		private DilationOfAscendingAortaFormula(Constants constants) {
			this.constants = constants;
		}
		private struct Constants {
			private PatientData Pd;
			public double Multiplier {
				get; private set;
			}
			public double Intercept {
				get; private set;
			}
			public double MSE {
				get; private set;
			}
			public double BSA => Pd.BSA;
			public Constants(double multiplier, double intercept, double mse, PatientData pd) {
				Multiplier = multiplier;
				Intercept = intercept;
				MSE = mse;
				Pd = pd;
			}
		}
	}
}
