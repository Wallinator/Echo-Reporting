using DICOMReporting.Data;
using DICOMReporting.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Reporting_Backend.Data {
	public class ReportSections {
		public string Situs = "";
		public string SystemicVeins = "";
		public string Atria = "";
		public string AVValves = "";
		public string Ventricles = "";
		public string Outlets = "";
		public string GreatArteries = "";
		public string PulmonaryVeins = "";
		public string CoronaryArteries = "";
		public string Other = "";
		public string Conclusion = "";
		public string SignOff = "";

		public ReportSections(StructuredReport sr) {
			Situs = OptionsToString(sr.ReportingOptions.Situs);
			SystemicVeins = OptionsToString(sr.ReportingOptions.SystemicVeins);
			SetAtriaText(sr);
			SetAVValvesText(sr);
			SetVentriclesText(sr);
			SetOutletsText(sr);
			SetGreatArteriesText(sr);
			PulmonaryVeins = OptionsToString(sr.ReportingOptions.PulmonaryVeins);
			SetCoronaryArteriesText(sr);
			Other += BoolResultToString(sr.ReportingOptions.NoPercardialEffusion);
			if (!sr.ReportingOptions.NoPercardialEffusion.Value) {
				Other += OptionsToString(sr.ReportingOptions.PercardialEffusion);
			}

			Conclusion = Conclusions(sr.ReportingOptions.Conclusion.Value);
			SignOff = SignOffs(sr.PatientData.ReportingDoctor.Value);
		}

		private void SetAtriaText(StructuredReport sr) {
			Atria += BoolResultToString(sr.ReportingOptions.AtriaSize);
			Atria += BoolResultToString(sr.ReportingOptions.AtriaSeptum);
			Atria += OptionsToString(sr.ReportingOptions.AtriaLeft);
			Atria += OptionsToString(sr.ReportingOptions.AtriaRight);
			Atria += OptionsToString(sr.ReportingOptions.AtriaBilateral);
			Atria += OptionsToString(sr.ReportingOptions.AtriaPatentForamenOvale);
			Atria += OptionsToString(sr.ReportingOptions.ASD1,
									 sr.ReportingOptions.ASD2,
									 sr.ReportingOptions.ASD3);
			Atria += ResultToString(sr.Results["Atrial Septal Defect dimension"]);
			Atria += ResultToString(sr.Results["Atrial Septal Defect mean gradient"]);
		}
		private void SetAVValvesText(StructuredReport sr) {
			AVValves += OptionsToString(sr.ReportingOptions.AVConnection);
			AVValves += OptionsToString(sr.ReportingOptions.MitralValve1,
										sr.ReportingOptions.MitralValve2,
										sr.ReportingOptions.MitralValve3);
			AVValves += BoolResultToString(sr.ReportingOptions.MitralValveProlapse);
			AVValves += OptionsToString(sr.ReportingOptions.LAVV1,
										sr.ReportingOptions.LAVV2,
										sr.ReportingOptions.LAVV3);
			AVValves += ResultToString(sr.Results["Mitral valve annulus"]);
			AVValves += ResultToString(sr.Results["Mitral valve E wave"]);
			AVValves += ResultToString(sr.Results["Mitral valve A wave"]);
			AVValves += ResultToString(sr.Results["Mitral E/A ratio"]);
			AVValves += ResultToString(sr.Results["Mitral valve inflow A wave duration"]);
			AVValves += ResultToString(sr.Results["MV decel time"]);
			AVValves += ResultToString(sr.Results["Mitral valve inflow mean gradient"]);
			AVValves += ResultToString(sr.Results["Mitral valve regurgitation peak velocity"]);
			AVValves += ResultToString(sr.Results["Mitral valve regurgitation peak gradient"]);
			AVValves += OptionsToString(sr.ReportingOptions.TriscupidValve1,
										sr.ReportingOptions.TriscupidValve2,
										sr.ReportingOptions.TriscupidValve3);
			AVValves += OptionsToString(sr.ReportingOptions.RAVV1,
										sr.ReportingOptions.RAVV2);
			AVValves += BoolResultToString(sr.ReportingOptions.InsufficientTR);
			AVValves += ResultToString(sr.Results["Tricuspid valve annulus"]);
			AVValves += ResultToString(sr.Results["Tricuspid valve inflow mean gradient"]);
			AVValves += ResultToString(sr.Results["Tricuspid valve regurgitation peak velocity"]);
			AVValves += ResultToString(sr.Results["Estimated RV systolic pressure"]);
		}
		private void SetVentriclesText(StructuredReport sr) {
			Ventricles += OptionsToString(sr.ReportingOptions.VentricleFunction);
			Ventricles += BoolResultToString(sr.ReportingOptions.VentricularHypertrophy);

			Ventricles += ResultToString(sr.Results["IVSd"]);
			Ventricles += ResultToString(sr.Results["LVIDd"]);
			Ventricles += ResultToString(sr.Results["LVPWd"]);
			Ventricles += ResultToString(sr.Results["LV mass index"]);
			Ventricles += ResultToString(sr.Results["Fractional Shortening"]);
			Ventricles += ResultToString(sr.Results["Left Ventricular Teichholz EF"]);
			string ejectionfraction = ResultToString(sr.Results["Left Ventricular biplane EF"]);
			if (ejectionfraction.Length == 0) {
				ejectionfraction = ResultToString(sr.Results["Left ventricular Apical 4 chamber EF"]);
			}
			Ventricles += ejectionfraction;

			Ventricles += ResultToString(sr.Results["Heart Rate"], false);

			Ventricles += OptionsToString(sr.ReportingOptions.DilatedLV);
			Ventricles += OptionsToString(sr.ReportingOptions.HypertrophiedLV);
			Ventricles += OptionsToString(sr.ReportingOptions.ReducedLVFunction);
			Ventricles += BoolResultToString(sr.ReportingOptions.LVSystolicFunction);
			Ventricles += BoolResultToString(sr.ReportingOptions.NormalDiastolic);
			Ventricles += ResultToString(sr.Results["LV IVRT"]);
			Ventricles += ResultToString(sr.Results["Myocardial Performance Index"]);

			Ventricles += ResultToString(sr.Results["Pulm vein S wave"]);
			Ventricles += ResultToString(sr.Results["Pulm vein D wave"]);
			Ventricles += ResultToString(sr.Results["Pulm vein A wave"]);

			Ventricles += ResultToString(sr.Results["Pulmonary vein A wave duration"]);
			Ventricles += ResultToString(sr.Results["Mitral valve inflow A wave duration"]);

			Ventricles += ResultToString(sr.Results["Mitral annulus E'"]);
			Ventricles += ResultToString(sr.Results["Mitral annulus A'"]);
			Ventricles += ResultToString(sr.Results["Mitral annulus S'"]);

			Ventricles += ResultToString(sr.Results["Septal annulus E'"]);
			Ventricles += ResultToString(sr.Results["Septal annulus A'"]);
			Ventricles += ResultToString(sr.Results["Septal annulus S'"]);

			Ventricles += ResultToString(sr.Results["Tricuspid annulus E'"]);
			Ventricles += ResultToString(sr.Results["Tricuspid annulus A'"]);
			Ventricles += ResultToString(sr.Results["Tricuspid annulus S'"]);
			Ventricles += OptionsToString(sr.ReportingOptions.DilatedRV);
			Ventricles += OptionsToString(sr.ReportingOptions.HypertrophiedRV);
			Ventricles += OptionsToString(sr.ReportingOptions.ReducedRVFunction);
			Ventricles += OptionsToString(sr.ReportingOptions.SeptalMotion);
			Ventricles += BoolResultToString(sr.ReportingOptions.IntactVentricularSeptum);
			Ventricles += BoolResultToString(sr.ReportingOptions.ResidualVSD);
			Ventricles += OptionsToString(sr.ReportingOptions.VSD1,
										  sr.ReportingOptions.VSD2,
										  sr.ReportingOptions.VSD3);
			Ventricles += ResultToString(sr.Results["Ventricular Septal Defect dimension"]);
			Ventricles += ResultToString(sr.Results["Ventricular Septal Defect peak velocity"]);
			Ventricles += ResultToString(sr.Results["Ventricular Septal Defect peak gradient"]);
		}
		private void SetOutletsText(StructuredReport sr) {
			Outlets += OptionsToString(sr.ReportingOptions.Ventriculoarterial);
			Outlets += BoolResultToString(sr.ReportingOptions.OutflowTracts);
			Outlets += BoolResultToString(sr.ReportingOptions.SubAorticMembrane);
			if (!sr.ReportingOptions.OutflowTracts.Value) {
				Outlets += ResultToString(sr.Results["Left ventricle outflow dimension"]);
				Outlets += ResultToString(sr.Results["Left ventricle outflow peak velocity"]);
				Outlets += ResultToString(sr.Results["Left ventricle outflow peak gradient"]);
				Outlets += ResultToString(sr.Results["Left ventricle outflow mean gradient"]);
			}

			Outlets += OptionsToString(sr.ReportingOptions.AorticValve1,
									   sr.ReportingOptions.AorticValve2,
									   sr.ReportingOptions.AorticValve3);

			Outlets += ResultToString(sr.Results["Aortic valve annulus"]);
			Outlets += ResultToString(sr.Results["Aortic valve peak velocity"]);
			Outlets += ResultToString(sr.Results["Aortic valve peak gradient"]);
			Outlets += ResultToString(sr.Results["Aortic valve mean gradient"]);
			Outlets += ResultToString(sr.Results["Aortic valve pressure half-time"]);

			Outlets += BoolResultToString(sr.ReportingOptions.AorticValveLeaflets);
			Outlets += BoolResultToString(sr.ReportingOptions.AorticValveProlapse);
			Outlets += BoolResultToString(sr.ReportingOptions.AortaVSDOvveride);
			Outlets += BoolResultToString(sr.ReportingOptions.LossSinotubularJunction);
			Outlets += OptionsToString(sr.ReportingOptions.SubPulmonaryStenosis);

			Outlets += ResultToString(sr.Results["Right ventricle outflow dimension"]);
			Outlets += ResultToString(sr.Results["Right ventricle outflow peak velocity"]);
			Outlets += ResultToString(sr.Results["Right ventricle outflow peak gradient"]);
			Outlets += ResultToString(sr.Results["Right ventricle outflow mean gradient"]);

			Outlets += OptionsToString(sr.ReportingOptions.PulmonaryValve1,
									   sr.ReportingOptions.PulmonaryValve2,
									   sr.ReportingOptions.PulmonaryValve3);

			Outlets += ResultToString(sr.Results["Pulmonary valve annulus"]);
			Outlets += ResultToString(sr.Results["Pulmonary valve peak velocity"]);
			Outlets += ResultToString(sr.Results["Pulmonary valve peak gradient"]);
			Outlets += ResultToString(sr.Results["Pulmonary valve mean gradient"]);
			Outlets += ResultToString(sr.Results["Pulmonary valve end diastolic velocity"]);
			Outlets += ResultToString(sr.Results["Pulmonary valve end diastolic peak gradient"]);
		}
		private void SetGreatArteriesText(StructuredReport sr) {
			GreatArteries += OptionsToString(sr.ReportingOptions.LeftAorticArch1,
														 sr.ReportingOptions.LeftAorticArch2);
			GreatArteries += ResultToString(sr.Results["Sinuses of Valsalva"]);
			GreatArteries += ResultToString(sr.Results["Sinotubular junction"]);
			GreatArteries += ResultToString(sr.Results["Ascending aorta"]);
			GreatArteries += ResultToString(sr.Results["Transverse aortic arch"]);
			GreatArteries += ResultToString(sr.Results["Distal aortic arch"]);
			GreatArteries += ResultToString(sr.Results["Aortic isthmus"]);
			GreatArteries += ResultToString(sr.Results["Ascending aorta peak velocity"]);
			GreatArteries += ResultToString(sr.Results["Ascending aorta peak gradient"]);

			GreatArteries += OptionsToString(sr.ReportingOptions.RightAorticArch1,
											 sr.ReportingOptions.RightAorticArch2);

			GreatArteries += BoolResultToString(sr.ReportingOptions.NoCoarctationAorta);
			if (!sr.ReportingOptions.NoCoarctationAorta.Value) {
				GreatArteries += OptionsToString(sr.ReportingOptions.CoarctationAorta);
			}
			GreatArteries += ResultToString(sr.Results["Coarctation of the aorta"]);

			GreatArteries += ResultToString(sr.Results["Descending aorta peak velocity"]);
			GreatArteries += ResultToString(sr.Results["Descending aorta peak gradient"]);

			GreatArteries += OptionsToString(sr.ReportingOptions.BranchPulmonaryArteries);

			GreatArteries += ResultToString(sr.Results["Main pulmonary artery"]);
			GreatArteries += ResultToString(sr.Results["Main pulmonary artery peak velocity"]);
			GreatArteries += ResultToString(sr.Results["Main pulmonary artery peak gradient"]);

			GreatArteries += ResultToString(sr.Results["Right pulmonary artery"]);
			GreatArteries += ResultToString(sr.Results["Right pulmonary artery peak velocity"]);
			GreatArteries += ResultToString(sr.Results["Right pulmonary artery peak gradient"]);

			GreatArteries += ResultToString(sr.Results["Left pulmonary artery"]);
			GreatArteries += ResultToString(sr.Results["Left pulmonary artery peak velocity"]);
			GreatArteries += ResultToString(sr.Results["Left pulmonary artery peak gradient"]);

			GreatArteries += BoolResultToString(sr.ReportingOptions.NoPatentDuctusArteriosus);
			if (!sr.ReportingOptions.NoPatentDuctusArteriosus.Value) {
				GreatArteries += OptionsToString(sr.ReportingOptions.PatentDuctusArteriosus1,
												 sr.ReportingOptions.PatentDuctusArteriosus2);
			}

			GreatArteries += ResultToString(sr.Results["Patent Ductus Arteriosus"]);
			GreatArteries += ResultToString(sr.Results["Patent Ductus Arteriosus peak velocity systole"]);
			GreatArteries += ResultToString(sr.Results["Patent Ductus Arteriosus peak velocity diastole"]);
		}
		private void SetCoronaryArteriesText(StructuredReport sr) {
			CoronaryArteries = OptionsToString(sr.ReportingOptions.CoronaryArteries);
			CoronaryArteries += ResultToString(sr.Results["Left Main Coronary Artery"]);
			CoronaryArteries += ResultToString(sr.Results["Anterior Descending Branch of Left Coronary Artery"]);
			CoronaryArteries += ResultToString(sr.Results["Right Coronary Artery"]);
			CoronaryArteries += ResultToString(sr.Results["Left Circumflex"]);
		}
		private string BoolResultToString(BoolResult r) {
			if (!r.Value) {
				return "";
			}
			else {
				string final = r.Name;
				return final.Length == 0 ? final : final + ". ";
			}
		}
		private string ResultToString(Result r, bool includeZScore = true) {
			if (r.Empty) {
				return "";
			}
			else {
				string final = r.ReportString(includeZScore);
				return final.Length == 0 ? final : final + ". ";
			}
		}
		private string OptionsToString(MultipleChoiceResult result1) {
			return OptionsToString(new List<MultipleChoiceResult>() { result1 });
		}
		private string OptionsToString(MultipleChoiceResult result1, MultipleChoiceResult result2) {
			return OptionsToString(new List<MultipleChoiceResult>() { result1, result2 });
		}
		private string OptionsToString(MultipleChoiceResult result1, MultipleChoiceResult result2, MultipleChoiceResult result3) {
			return OptionsToString(new List<MultipleChoiceResult>() { result1, result2, result3 });
		}
		private string OptionsToString(List<MultipleChoiceResult> list) {
			string final = "";
			foreach(var result in list) {
				if (result.Value.Length == 0) {
					break;
				}
				else {
					final += result.AsString();
				}
			}
			final = final.Trim();
			return final.Length == 0 ? final : final + ". ";
		}
		private string Conclusions(string type) {
			switch (type) {
				case "Normal":
					return "Structurally and functionally normal heart. Normal valvular function, unobstructed outflow tracts and left sided aortic arch. No septal defects and no PDA. Normal systemic and pulmonary venous connections";
				case "Normal with PFO":
					return "Structurally and functionally normal heart. Normal valvular function, unobstructed outflow tracts and left sided aortic arch. Patent foramen ovale with L-R shunt. No ventricular septal defect and no PDA. Normal systemic and pulmonary venous connections";
				case "Muscular VSD":
					return "Functionally normal heart. muscular VSD with L-R shunt. Normal estimated RV pressure for age. Normal valvular function, unobstructed outflow tracts and left sided aortic arch. Intact atrial septum, no PDA. Normal systemic and pulmonary venous connections";

				case "Muscular VSD with PFO":
					return "Functionally normal heart. muscular VSD with L-R shunt. Normal estimated RV pressure for age. Normal valvular function, unobstructed outflow tracts and left sided aortic arch. Patent foramen ovale with L - R shunt. No PDA. Normal systemic and pulmonary venous connections";

				case "Perimembranous VSD":
					return "Functionally normal heart. perimembranous VSD partly closed by tricuspid valvular aneurysmal tissue with L-R shunt. Normal estimated RV pressure for age. Normal valvular function, no aortic valve prolapse or regurgitation. Unobstructed outflow tracts and left sided aortic arch. Intact atrial septum and no PDA. Normal systemic and pulmonary venous connections";

				case "Perimembranous VSD with PFO":
					return "Functionally normal heart. perimembranous VSD partly closed by tricuspid valvular aneurysmal tissue with L-R shunt.Normal estimated RV pressure for age. Normal valvular function, no aortic valve prolapse or regurgitation. Unobstructed outflow tracts and left sided aortic arch. Patent foramen ovale with L-R shunt. No PDA. Normal systemic and pulmonary venous connections";

				case "Pulmonary stenosis":
					return "Normal biventricular dimensions and function. Mild pulmonary valve stenosis. Normal aortic, mitral and tricuspid valvular function. Unobstructed left sided left sided aortic arch. No septal defects and no PDA. Normal systemic and pulmonary venous connections";
				default:
					return "";
			}
		}
		private string SignOffs(string doctor) {
			switch (doctor) {
				case "Dr Paul Brooks":
					return "Dr Paul Brooks MBBS (Hons) FRACP. Provider number 223704PX. LSPN 0008492";
				default:
					return "";
			}
		}
	}
}
