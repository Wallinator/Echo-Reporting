using DICOMReporting.Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DICOMReporting.Data {
	public class ReportingOptions {
		//Situs
		public MultipleChoiceResult Situs = new MultipleChoiceResult("", new List<string>() {	"Usual atrial arrangement (situs solitus)",
																									"Mirror image arrangement (situs inversus)",
																									"Left atrial isomerism",
																									"Right atrial isomerism",
																									"Situs ambiguous",
																									"Not evaluated" });

		//Systemic Veins
		public MultipleChoiceResult SystemicVeins = new MultipleChoiceResult("", new List<string>() { "Normal systemic venous connections",
																													"Bilateral SVC's with LSVC to coronary sinus",
																													"Isolated left SVC draining to the coronary sinus",
																													"Interrupted IVC with azygous continuation",
																													"Not evaluated" });

		//Atria
		public BoolResult AtriaSize = new BoolResult("Normally sized atria", "", true);
		public BoolResult AtriaSeptum = new BoolResult("Intact atrial septum", "", true);

		public MultipleChoiceResult AtriaLeft = new MultipleChoiceResult("", new List<string>() {   "",
																									"Mildly",
																									"Moderately",
																									"Severely" }, "dilated left atrium");

		public MultipleChoiceResult AtriaRight = new MultipleChoiceResult("", new List<string>() {  "",
																									"Mildly",
																									"Moderately",
																									"Severely" }, "dilated right atrium");

		public MultipleChoiceResult AtriaBilateral = new MultipleChoiceResult("", new List<string>() {  "",
																										"Mildly",
																										"Moderately",
																										"Severely" }, "dilated atria bilaterally");
		
		public MultipleChoiceResult AtriaPatentForamenOvale = new MultipleChoiceResult("Patent foramen ovale with", new List<string>() {"",
																																		"L-R shunt",
																																		"R-L shunt",
																																		"bidirectional shunt" });
		public MultipleChoiceResult ASD1 = new MultipleChoiceResult("", new List<string>() {"",
																							"Small",
																							"Moderate",
																							"Large" });

		public MultipleChoiceResult ASD2 = new MultipleChoiceResult("", new List<string>() {"secundum ASD",
																							"primum ASD",
																							"superior sinus venousus defect with partial anomalous pulmonary venous drainage" });
		
		public MultipleChoiceResult ASD3 = new MultipleChoiceResult(" with", new List<string>() {"L-R shunt",
																								"R-L shunt",
																								"bidirectional shunt" });

		//AV Valves
		public MultipleChoiceResult AVConnection	 = new MultipleChoiceResult("", new List<string>() {   "Atrioventricular concordance",
																											"Atrioventricular discordance" });
		
		
		public MultipleChoiceResult MitralValve1 = new MultipleChoiceResult("", new List<string>() {"Normal mitral valve",
																									"",
																									"Cleft in the anterior leaflet of the mitral valve",
																									"Prolapse of the anterior leaflet of the mitral valve"});
		
		public MultipleChoiceResult MitralValve2 = new MultipleChoiceResult(" with", new List<string>() {"no regurgitation",
																										"trivial regurgitation",
																										"mild regurgitation",
																										"moderate regurgitation",
																										"severe regurgitation" });

		public MultipleChoiceResult MitralValve3 = new MultipleChoiceResult(" and", new List<string>() {	"no stenosis",
																										"mild stenosis",
																										"moderate stenosis",
																										"severe stenosis" });
		public BoolResult MitralValveProlapse = new BoolResult("No mitral valve prolapse");

		public MultipleChoiceResult TriscupidValve1 = new MultipleChoiceResult("", new List<string>() {     "Normal tricuspid valve",
																											"",
																											"Tethered tricuspid valve septal leaflet",
																											"Dysplastic tricuspid valve leaflets"});
		
		public MultipleChoiceResult TriscupidValve2 = new MultipleChoiceResult(" with", new List<string>() {	"no regurgitation",
																											"trivial regurgitation",
																											"mild regurgitation",
																											"moderate regurgitation",
																											"severe regurgitation" });

		public MultipleChoiceResult TriscupidValve3 = new MultipleChoiceResult(" and", new List<string>() {	"no stenosis",
																											"mild stenosis",
																											"moderate stenosis",
																											"severe stenosis" });
		public BoolResult InsufficientTR = new BoolResult("Insufficient TR to estimate RV pressure");

		

		public MultipleChoiceResult LAVV1 = new MultipleChoiceResult("", new List<string>() {   "",
																								"Common atrioventricular valve",
																								"Partial AVSD with separate AV valve orifices"});

		public MultipleChoiceResult LAVV2 = new MultipleChoiceResult(" with", new List<string>() {   "no left AV valve regurgitation",
																									"trivial left AV valve regurgitation",
																									"mild left AV valve regurgitation",
																									"moderate left AV valve regurgitation",
																									"severe left AV valve regurgitation" });

		public MultipleChoiceResult LAVV3 = new MultipleChoiceResult(" and", new List<string>() {    "no left AV valve stenosis",
																									"mild left AV valve stenosis",
																									"moderate left AV valve stenosis",
																									"severe left AV valve stenosis" });

		public MultipleChoiceResult RAVV1 = new MultipleChoiceResult("", new List<string>() {   "",
																								"No right AV valve regurgitation",
																								"Trivial right AV valve regurgitation",
																								"Mild right AV valve regurgitation",
																								"Moderate right AV valve regurgitation",
																								"Severe right AV valve regurgitation" });

		public MultipleChoiceResult RAVV2 = new MultipleChoiceResult(" and", new List<string>() {    "no right AV valve stenosis",
																									"mild right AV valve stenosis",
																									"moderate right AV valve stenosis",
																									"severe right AV valve stenosis" });

		//Ventricles

		public MultipleChoiceResult VentricleFunction = new MultipleChoiceResult("", new List<string>() {	"Normal biventricular size and function",
																											"Normal LV function",
																											"Normal RV function" });
		public BoolResult VentricularHypertrophy = new BoolResult("No ventricular hypertrophy");


		public MultipleChoiceResult DilatedLV = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "dilated LV");

		public MultipleChoiceResult HypertrophiedLV = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "hypertrophied LV");

		public MultipleChoiceResult ReducedLVFunction = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "reduced LV function");

		public BoolResult LVSystolicFunction = new BoolResult("Hyperdynamic LV systolic function");
		public BoolResult NormalDiastolic = new BoolResult("Normal diastolic functional parameters");

		public MultipleChoiceResult DilatedRV = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "dilated RV");

		public MultipleChoiceResult HypertrophiedRV = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "hypertrophied RV");

		public MultipleChoiceResult ReducedRVFunction = new MultipleChoiceResult("", new List<string>() { "",
																							"Mildly",
																							"Moderately",
																							"Severely" }, "reduced RV function");

		public MultipleChoiceResult SeptalMotion = new MultipleChoiceResult("", new List<string>() {"Normal septal curvature",
																									"Flattened septal motion",
																									"Diastolic septal flattening",
																									"Dyskinetic septal motion" });

		public BoolResult IntactVentricularSeptum = new BoolResult("Intact ventricular septum", "", true);
		public BoolResult ResidualVSD = new BoolResult("No residual VSD");

		public MultipleChoiceResult VSD1 = new MultipleChoiceResult("", new List<string>() {"",
																							"Small",
																							"Moderate",
																							"Large" });

		public MultipleChoiceResult VSD2 = new MultipleChoiceResult("", new List<string>() {    "perimembranous VSD",
																								"anterior muscular VSD",
																								"apical muscular VSD",
																								"inlet muscular VSD",
																								"outlet VSD",
																								"doubly committed VSD" });

		public MultipleChoiceResult VSD3 = new MultipleChoiceResult(" with", new List<string>() {"L-R shunt",
																								"R-L shunt",
																								"bidirectional shunt" });
		//Outlets

		public MultipleChoiceResult Ventriculoarterial = new MultipleChoiceResult("", new List<string>() {  "Ventriculoarterial concordance",
																											"Ventriculoarterial discordance" });
		
		public BoolResult OutflowTracts = new BoolResult("Unobstructed outflow tracts", "", true);
		public BoolResult SubAorticMembrane = new BoolResult("Sub-aortic membrane");

		public MultipleChoiceResult AorticValve1 = new MultipleChoiceResult("", new List<string>() {    "Trileaflet aortic valve",
																										"Bicuspid aortic valve"});

		public MultipleChoiceResult AorticValve2 = new MultipleChoiceResult(" with", new List<string>() {  "no stenosis",
																											"mild stenosis",
																											"moderate stenosis",
																											"severe stenosis" }); 

		public MultipleChoiceResult AorticValve3 = new MultipleChoiceResult(" and", new List<string>() { "no regurgitation",
																										"trivial regurgitation",
																										"mild regurgitation",
																										"moderate regurgitation",
																										"severe regurgitation" });
		public BoolResult AorticValveLeaflets = new BoolResult("Thickened aortic valve leaflets");
		public BoolResult AorticValveProlapse = new BoolResult("No aortic valve prolapse"); 
		public BoolResult AortaVSDOvveride = new BoolResult("Aorta overrides the VSD");
		public BoolResult LossSinotubularJunction = new BoolResult("Loss of the sinotubular junction");

		public MultipleChoiceResult SubPulmonaryStenosis = new MultipleChoiceResult("", new List<string>() {    "",
																												"Muscular",
																												"Fibromuscular"}, "sub-pulmonary stenosis");

		public MultipleChoiceResult PulmonaryValve1 = new MultipleChoiceResult("", new List<string>() { "Normal pulmonary valve",
																										"Thickened and doming pulmonary valve leaflets"});

		public MultipleChoiceResult PulmonaryValve2 = new MultipleChoiceResult(" with", new List<string>() {  "no stenosis",
																											"mild stenosis",
																											"moderate stenosis",
																											"severe stenosis" });
		
		public MultipleChoiceResult PulmonaryValve3 = new MultipleChoiceResult(" and", new List<string>() { "no regurgitation",
																										"trivial regurgitation",
																										"mild regurgitation",
																										"moderate regurgitation",
																										"severe regurgitation" });
		//Great Arteries
		public MultipleChoiceResult LeftAorticArch1 = new MultipleChoiceResult("", new List<string>() { "Unobstructed left sided aortic arch",
																										"Left sided aortic arch",
																										"" });

		public MultipleChoiceResult LeftAorticArch2 = new MultipleChoiceResult("", new List<string>() { "with normal branching",
																										"",
																										"with bovine trunk (common origin of the brachiocephalic and LCCA)",
																										"aberrant right subclavian artery" });
		// right aortic arch label
		public MultipleChoiceResult RightAorticArch1 = new MultipleChoiceResult("", new List<string>() {"",
																										"Unobstructed right sided aortic arch",
																										"Right sided aortic arch"}, displayname: "Right aortic arch");

		public MultipleChoiceResult RightAorticArch2 = new MultipleChoiceResult("", new List<string>() {"",
																										"with mirror image branching",
																										"aberrant left subclavian artery (with a left sided ductal ligament this is the setup for a vascular ring)" });

		public BoolResult NoCoarctationAorta = new BoolResult("No coarctation of the aorta", "", true);

		public MultipleChoiceResult CoarctationAorta = new MultipleChoiceResult("Coarctation of the aorta with", new List<string>() {	"diastolic run-off ",
																																		"no diastolic run-off" });

		public MultipleChoiceResult BranchPulmonaryArteries = new MultipleChoiceResult("", new List<string>() { "Normal branch pulmonary arteries",
																												"",
																												"Physiological branch pulmonary artery narrowing" });
		
		public BoolResult NoPatentDuctusArteriosus = new BoolResult("No patent ductus arteriosus", "", true);

		public MultipleChoiceResult PatentDuctusArteriosus1 = new MultipleChoiceResult("", new List<string>() {	"",
																												"Small",
																												"Moderate",
																												"Large" });

		public MultipleChoiceResult PatentDuctusArteriosus2 = new MultipleChoiceResult("patent ductus arteriosus with", new List<string>() {"L-R shunt",
																																			"R-L shunt",
																																			"bidirectional shunt" });
		//Pulmonary Veins

		public MultipleChoiceResult PulmonaryVeins = new MultipleChoiceResult("", new List<string>() {  "Normal pulmonary venous connection",
																										"Partial anomalous pulmonary venous drainage",
																										"Not evaluated" });

		//Coronary Arteries
		public MultipleChoiceResult CoronaryArteries = new MultipleChoiceResult("", new List<string>() {"Normal proximal coronary origins",
																										"LAD not well seen",
																										"Circumflex not well seen",
																										"RCA not well seen",
																										"Not evaluated" }, "", true );
		//Other
		public BoolResult NoPercardialEffusion = new BoolResult("No percardial effusion", "", true);

		public MultipleChoiceResult PercardialEffusion = new MultipleChoiceResult("", new List<string>() {  "",
																											"Small",
																											"Moderate",
																											"Large" }, "percardial effusion" );
		// Conclusion
		public MultipleChoiceResult Conclusion = new MultipleChoiceResult("Conclusion:", new List<string>() {	"Normal",
																												"Normal with PFO",
																												"Muscular VSD",
																												"Muscular VSD with PFO",
																												"Perimembranous VSD",
																												"Perimembranous VSD with PFO",
																												"Pulmonary stenosis"});
	}
}
