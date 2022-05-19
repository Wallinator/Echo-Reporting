using Dicom.Network.Client.EventArguments;
using DICOMReporting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo_Reporting_Backend.Data {
	public static class ReportGenerator {
		public static string GenerateHTML(StructuredReport report, int fontSize) {

			List<string> l1, l2, l3;
			var html = @"<html>
				<head>
				<link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"" integrity=""sha384-JcKb8q3iqJ61gNV9KGb8thSsNjpSL0n8PARn9HuZOnIxN0hoP+VmmDGMN5t9UJ0Z"" crossorigin=""anonymous"">
				<script src = ""https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"" integrity = ""sha384-B4gt1jrGC7Jh4AgTPSdUtOBvfO8shuf57BaghqFfPlYxofvL8/KUEfYiJOMMV+rV"" crossorigin = ""anonymous""></script>
				</head>";

			html += string.Format("<body style = \"font-size: {0}px;\">", fontSize);
			html += @"<p></p><img src=""https://i.ibb.co/k5KJw8p/letterheadplain2.jpg"" width=""100%"">
			<p><span style = ""font-size: 24px; font-weight: 700; text-decoration-line: underline;"">Echocardiogram Report</span><br />";
			/*html += @"<p></p>
			<br/>
			<br/>
			<br/>
			<br/>
			<br/>
			<br/>
			<br/>";*/
			html += string.Format("<table style = \"border-collapse: collapse; font-size: {0}px; width: 100%;\" cellpadding = \"0\"><tbody>", fontSize);
			var pd = report.PatientData;
			string age = Math.Floor(pd.PatientAge.Value).ToString();
			if (pd.PatientAge.Value < 1) {
				age = Math.Floor(pd.PatientAge.Value * 12).ToString() + " mo";
			}
			string bp = "";
			if (!pd.SystolicBloodPressure.Empty && !pd.DiastolicBloodPressure.Empty) {
				bp = pd.SystolicBloodPressure.Value.ToString("N0") + "/" + pd.DiastolicBloodPressure.Value.ToString("N0") + " mmHg";
			}

			l1 = pd.PatientID.TableString();
			l2 = pd.StudyDate.TableString();
			html += AddPatientDataRow(l1, l2);
			l1 = pd.PatientName.TableString();
			l2 = pd.PatientHeight.TableString();
			html += AddPatientDataRow(l1, l2);
			l1 = pd.PatientSex.TableString();
			l2 = pd.PatientWeight.TableString();
			html += AddPatientDataRow(l1, l2);
			l1 = pd.PatientDOB.TableString();
			l2 = pd.BSA.TableString();
			html += AddPatientDataRow(l1, l2);
			l1 = new List<string>() { pd.PatientAge.Name, age };
			l2 = new List<string>() { "Blood Pressure", bp };
			html += AddPatientDataRow(l1, l2);
			l1 = pd.ReasonForStudy.TableString();
			l2 = pd.EchoType.TableString();
			html += AddPatientDataRow(l1, l2);
			l1 = pd.ReferringPhysician.TableString();
			l2 = new List<string>() { "", "" };
			html += AddPatientDataRow(l1, l2);


			html += @"
			</tbody>
			</table><br/>
			<p><span style = ""font-size: 24px; font-weight: 700; text-decoration-line: underline;"">Findings</span><br />
			</p>";
			html += string.Format("<table style = \"border-collapse: collapse; font-size: {0}px;\" class=\"table table-bordered table-sm\">", fontSize);
			html += @"
				<thead class=""thead-light"">
					  <tr>
						<th style = ""text-align: left;""> Measure </th>
						<th style=""text-align: left;"">(cm)<span style = ""white-space:pre""></span></th>
						<th style=""text-align: left;"">Z Score</th>
						<th style = ""text-align: left;""> Measure </th>
						<th style= ""text-align: left;""></th>
						<th style= ""text-align: left;""> Calculation </th>
						<th style= ""text-align: left;""></th>
					</tr>
				</thead>
				<tbody>";
			l1 = report.Results["IVSd"].AsString();
			l2 = report.Results["Mitral valve E wave"].AsString();
			l3 = report.Results["Fractional Shortening"].AsString(1);
			l2[0] = "MV Peak E";
			l3[0] = "FS%";
			html += AddTableRow(l1, l2, l3);

			l1 = report.Results["LVIDd"].AsString();
			l2 = report.Results["Mitral valve A wave"].AsString();
			l3 = report.Results["Left Ventricular Teichholz EF"].AsString(1);
			l2[0] = "MV Peak A";
			l3[0] = "EF (Teich)";
			html += AddTableRow(l1, l2, l3);

			l1 = report.Results["LVPWd"].AsString();
			l2 = report.Results["MV decel time"].AsString(0);
			l3 = report.Results["LV mass index"].AsString(1);
			l2[0] = "MV decel time";
			l3[0] = "LV Mass Index (Cubed)";
			html += AddTableRow(l1, l2, l3);

			l1 = report.Results["IVSs"].AsString();
			l2 = report.Results["Mitral annulus E'"].AsString();
			l3 = report.Results["Heart Rate"].AsString(0);
			l2[0] = "LV E'";
			l3[0] = "HR";
			html += AddTableRow(l1, l2, l3);

			l1 = report.Results["LVIDs"].AsString();
			l2 = report.Results["Septal annulus E'"].AsString();
			l3 = report.Results["MVCFc"].AsString();
			l2[0] = "Septal E'";
			l3[0] = "MVCFc";
			html += AddTableRow(l1, l2, l3);

			l1 = report.Results["LVPWs"].AsString();
			l2 = report.Results["Tricuspid annulus E'"].AsString();
			l3 = report.Results["Left ventricular cardiac output"].AsString();
			l2[0] = "RV E'";
			l3[0] = "LV output";
			html += AddTableRow(l1, l2, l3);


			html += @"
				</tbody>
			</table>
			<p></p>
			<p></p>";
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Situs - </span>{0}", report.Sections.Situs);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Systemic Veins - </span>{0}</div> ", report.Sections.SystemicVeins);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Atria - </span>{0}</div> ", report.Sections.Atria);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> AV Valves - </span>{0}</div></div> ", report.Sections.AVValves);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Ventricles - </span>{0}", report.Sections.Ventricles);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Outlets - </span>{0}</div> ", report.Sections.Outlets);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Great Arteries - </span>{0}</div> ", report.Sections.GreatArteries);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Pulmonary Veins - </span>{0}</div> ", report.Sections.PulmonaryVeins);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Coronary Arteries - </span>{0}</div> ", report.Sections.CoronaryArteries);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Other - </span>{0}</div> ", report.Sections.Other);

			html += @"<div></div><br/><div><span style = ""font-weight: bold; text-decoration-line: underline;""> Conclusion </span></div>";
			html += string.Format(@"<div>{0}</div><div><br/>", report.Sections.Conclusion);
			html += string.Format(@"<div><span style = ""font-weight: bold;""> Reporting - </span>{0}</div>", report.Sections.SignOff);
			html += "<br/></div></div><p></p><p></p></body></html>";
			return html;
		}

		private static string AddPatientDataRow(List<string> l1, List<string> l2) {
			string html = "";
			html += string.Format("<tr><td style = \"height: 0px; width: 0px;\">{0}</td>", l1[0]);
			html += "<td style = \"text-align: center; height: 0px; width: 0px;\"> -</td>";
			html += string.Format("<td style = \"text-align: right; height: 0px; width: 0px;\">{0}</td>", l1[1]);
			html += @" <td style = ""height: 0px; width: 0px; text-align: center;""> 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<span style = ""white-space:pre""></span></td>";
			html += string.Format("<td style = \"height: 0px; width: 0px;\">{0}</td>", l2[0]);
			html += "<td style = \"text-align: center; height: 0px; width: 0px;\"> -</td>";
			html += string.Format("<td style = \"text-align: right; height: 0px; width: 0px;\">{0}</td></tr>", l2[1]);
			return html;
		}
		private static string AddTableRow(List<string> l1, List<string> l2, List<string> l3) {
			string html = "";
			html += "</tr>";
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l1[0]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l1[1]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l1[3]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l2[0]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l2[1] + " " + l2[2]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l3[0]);
			html += string.Format("<td style = \"text-align: left;\">{0}</td>", l3[1] + " " + l3[2]);
			html += "</tr>";
			return html;
		}
	}
}
