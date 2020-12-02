using Echo_Reporting_Backend.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DICOMReporting.Data;
using PuppeteerSharp;
using System.Diagnostics;
using PuppeteerSharp.Media;
using System.Windows.Forms.VisualStyles;

namespace Echo_Reporting_Windows_App {
	public partial class ReportForm : Form {
		private StructuredReport _report;
		public ReportForm(StructuredReport report) {
			InitializeComponent();
			_report = report;
			SitusTextBox.Text = _report.Sections.Situs;
			SitusTextBox.TextChanged += new EventHandler(_TextChanged);

			SystemicVeinsTextbox.Text = _report.Sections.SystemicVeins;
			SystemicVeinsTextbox.TextChanged += new EventHandler(_TextChanged);

			AtriaTextBox.Text = _report.Sections.Atria;
			AtriaTextBox.TextChanged += new EventHandler(_TextChanged);

			AVValvesTextBox.Text = _report.Sections.AVValves;
			AVValvesTextBox.TextChanged += new EventHandler(_TextChanged);

			VentriclesTextBox.Text = _report.Sections.Ventricles;
			VentriclesTextBox.TextChanged += new EventHandler(_TextChanged);

			OutletsTextBox.Text = _report.Sections.Outlets;
			OutletsTextBox.TextChanged += new EventHandler(_TextChanged);

			GreatArteriesTextBox.Text = _report.Sections.GreatArteries;
			GreatArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			PulmonaryVeinsTextBox.Text = _report.Sections.PulmonaryVeins;
			PulmonaryVeinsTextBox.TextChanged += new EventHandler(_TextChanged);

			CoronaryArteriesTextBox.Text = _report.Sections.CoronaryArteries;
			CoronaryArteriesTextBox.TextChanged += new EventHandler(_TextChanged);

			OtherTextBox.Text = _report.Sections.Other;
			OtherTextBox.TextChanged += new EventHandler(_TextChanged);

			ConclusionTextBox.Text = _report.Sections.Conclusion;
			ConclusionTextBox.TextChanged += new EventHandler(_TextChanged);

			ReportingTextBox.Text = _report.Sections.SignOff;
			ReportingTextBox.TextChanged += new EventHandler(_TextChanged);
		}

		private void _TextChanged(object sender, EventArgs e) {
			_report.Sections.Situs = SitusTextBox.Text;
			_report.Sections.SystemicVeins = SystemicVeinsTextbox.Text;
			_report.Sections.Atria = AtriaTextBox.Text;
			_report.Sections.AVValves = AVValvesTextBox.Text;
			_report.Sections.Ventricles = VentriclesTextBox.Text;
			_report.Sections.Outlets = OutletsTextBox.Text;
			_report.Sections.GreatArteries = GreatArteriesTextBox.Text;
			_report.Sections.PulmonaryVeins = PulmonaryVeinsTextBox.Text;
			_report.Sections.CoronaryArteries = CoronaryArteriesTextBox.Text;
			_report.Sections.Other = OtherTextBox.Text;
			_report.Sections.Conclusion = ConclusionTextBox.Text;
			_report.Sections.SignOff = ReportingTextBox.Text;
		}

		private void ReportForm_Load(object sender, EventArgs e) {

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

		}

		private void GenerateReport(bool asPDF) {
			var name = _report.PatientData.PatientID.Value;
			// ch
			var date = _report.PatientData.StudyDate.Value.Replace("/","-");
			saveFileDialog1.FileName = name + " Echo " + date;
			if (asPDF) {
				saveFileDialog1.DefaultExt = "pdf";
				saveFileDialog1.Filter = "Pdf Files(*.pdf)| *.pdf | All files | *.*";
			}
			else {
				saveFileDialog1.DefaultExt = "html";
				saveFileDialog1.Filter = "Html Files(*.html)| *.html | All files | *.*";
			}
			var html = ReportGenerator.GenerateHTML(_report);
			if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
				var stream = saveFileDialog1.OpenFile();
				if (stream != null) {
					if (asPDF) {
						stream.Close();
						PuppeteerToPDF(html, saveFileDialog1.FileName);
					}
					else {
						_report.WriteGeneratedReport(stream, html);
						stream.Close();
					}
				}
				else {
					MessageBox.Show("The File Saving has failed.", "Error - Cannot Save File", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}/*
		private void SpireToPDF(string html, Stream stream) {
			var doc = new PdfDocument();
			var format = new PdfHtmlLayoutFormat();
			format.IsWaiting = false;
			var settings = new PdfPageSettings();
			settings.Size = PdfPageSize.A4;
			doc.LoadFromHTML(html, false, settings, format);
			doc.SaveToStream(stream, FileFormat.PDF);

		}
		private void GemBoxToPDF(string html, Stream stream) {
			var doc = new DocumentModel();
			//doc.Content.LoadText(html, new HtmlLoadOptions());
			doc.Save(stream, new PdfSaveOptions());
		}*/
		private async void PuppeteerToPDF(string html, string fileName) {
				await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
			var browser = await Puppeteer.LaunchAsync(new LaunchOptions {
				Headless = true
			});
			using (var page = await browser.NewPageAsync()) {
				await page.SetContentAsync(html);
				var result = await page.GetContentAsync();
				Debug.WriteLine("browser result: " + result);
				var options = new PdfOptions();
				var margins = new MarginOptions();
				options.MarginOptions.Left = "25";
				options.MarginOptions.Right = "25";
				await page.PdfAsync(fileName, options);
			}
		}
		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

		}

		private void asPDFToolStripMenuItem_Click(object sender, EventArgs e) {
			GenerateReport(true);
		}

		private void asHTMLToolStripMenuItem_Click(object sender, EventArgs e) {
			GenerateReport(false);
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {

		}

		private void SitusTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void ConclusionTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label12_Click(object sender, EventArgs e) {

		}

		private void ReportingTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label99_Click(object sender, EventArgs e) {

		}

		private void OtherTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label10_Click(object sender, EventArgs e) {

		}

		private void CoronaryArteriesTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label9_Click(object sender, EventArgs e) {

		}

		private void PulmonaryVeinsTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label8_Click(object sender, EventArgs e) {

		}

		private void GreatArteriesTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label6_Click(object sender, EventArgs e) {

		}

		private void OutletsTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label7_Click(object sender, EventArgs e) {

		}

		private void VentriclesTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label5_Click(object sender, EventArgs e) {

		}

		private void AVValvesTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label4_Click(object sender, EventArgs e) {

		}

		private void AtriaTextBox_TextChanged(object sender, EventArgs e) {

		}

		private void label1_Click(object sender, EventArgs e) {

		}

		private void label2_Click(object sender, EventArgs e) {

		}

		private void SystemicVeinsTextbox_TextChanged(object sender, EventArgs e) {

		}

		private void label3_Click(object sender, EventArgs e) {

		}

		private void panel1_Paint(object sender, PaintEventArgs e) {

		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void generateReportToolStripMenuItem_Click(object sender, EventArgs e) {

		}

		private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {

		}
	}
}
