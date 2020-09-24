﻿using DICOMReporting.Data;
using DICOMReporting.DicomFileReading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Echo_Reporting_Windows_App {
	public partial class MainForm : Form {
		private StructuredReport report;
		public MainForm() {
			InitializeComponent();
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e) {
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
				var folder = folderBrowserDialog1.SelectedPath;
				var enumerator = new DicomFileEnumerator(new DirectoryInfo(folder));
				if (enumerator.MoveNext()) {
					Debug.WriteLine(enumerator.Current.File.Name);
					DicomReader.GetStructuredReport(enumerator.Current.Dataset);
				}
			}
		}

		private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
			if (openFileDialog1.ShowDialog() == DialogResult.OK) {
				var path = openFileDialog1.FileName;
				var file = DicomFileEnumerator.TryOpen(path);
				if (file != null) {
					report = DicomReader.GetStructuredReport(file.Dataset);
					ShowAllResults();
				}
				else {
					MessageBox.Show("The selected file is invalid or in an unsupported format.", "Error - Invalid Dicom File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
		private void ShowAllResults() {
			PatientIDBox.Text = report.PatientData.PatientID;
			PatientNameBox.Text = report.PatientData.PatientName;
			PatientSexBox.Text = report.PatientData.PatientSex;
			AgePanel.Controls.Add(new ResultControl(new Result(report.PatientData.PatientAge)));
			HeightPanel.Controls.Add(new ResultControl(new Result(report.PatientData.PatientSize)));
			WeightPanel.Controls.Add(new ResultControl(new Result(report.PatientData.PatientWeight)));
			DiastolicBPPanel.Controls.Add(new ResultControl(new Result(report.PatientData.DiastolicBloodPressure)));
			SystolicBPPanel.Controls.Add(new ResultControl(new Result(report.PatientData.SystolicBloodPressure)));
		}

		private void MainForm_Load(object sender, EventArgs e) {

		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void textBox2_TextChanged(object sender, EventArgs e) {

		}
	}
}
