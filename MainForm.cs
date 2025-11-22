using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace KnowledgeSharingRecorder
{
    public partial class MainForm : Form
    {
        private const string CsvFileName = "KnowledgeSharingRecords.csv";
        private string CsvFilePath => Path.Combine(Application.StartupPath, CsvFileName);

        public MainForm()
        {
            InitializeComponent();
            
            // Initialize date pickers to today
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            
            // Initialize result label
            lblResult.Text = "Total sharings: --";
        }

        /// <summary>
        /// Handles the Save Record button click event.
        /// Validates the URL, saves it to CSV with timestamp, and provides user feedback.
        /// </summary>
        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            // Validate URL is not empty
            string url = txtUrl.Text.Trim();
            if (string.IsNullOrEmpty(url))
            {
                string errorMessage = "URL cannot be empty.";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = errorMessage;
                return;
            }

            try
            {
                // Get current timestamp in the required format
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Check if CSV file exists, if not create it with header
                bool fileExists = File.Exists(CsvFilePath);
                
                using (StreamWriter writer = new StreamWriter(CsvFilePath, append: true))
                {
                    // Write header if file is new
                    if (!fileExists)
                    {
                        writer.WriteLine("timestamp,url");
                    }
                    
                    // Append the new record
                    writer.WriteLine($"{timestamp},{url}");
                }

                // Clear the URL textbox
                txtUrl.Clear();

                // Show success message
                string successMessage = "Record saved successfully.";
                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = successMessage;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error saving record: {ex.Message}";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = errorMessage;
            }
        }

        /// <summary>
        /// Handles the Open CSV button click event.
        /// Opens the CSV file with the default associated application (e.g., Excel).
        /// </summary>
        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(CsvFilePath))
                {
                    string errorMessage = "CSV file not found.";
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = errorMessage;
                    return;
                }

                // Open the CSV file with the default associated application
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = CsvFilePath,
                    UseShellExecute = true
                };
                Process.Start(startInfo);

                lblStatus.Text = "CSV file opened.";
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error opening CSV file: {ex.Message}";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = errorMessage;
            }
        }

        /// <summary>
        /// Handles the Calculate Sharing Count button click event.
        /// Reads the CSV file, filters records by date range, and displays the count.
        /// </summary>
        private void btnCalculateCount_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if CSV file exists
                if (!File.Exists(CsvFilePath))
                {
                    string errorMessage = "CSV file not found.";
                    MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblStatus.Text = errorMessage;
                    lblResult.Text = "Total sharings: --";
                    return;
                }

                // Read all lines from CSV, skipping the header
                string[] lines = File.ReadAllLines(CsvFilePath);
                if (lines.Length <= 1)
                {
                    // Only header or empty file
                    lblResult.Text = "Total sharings: 0";
                    lblStatus.Text = "No records found in CSV file.";
                    return;
                }

                // Parse date range
                DateTime startDate = dtpStartDate.Value.Date; // 00:00:00
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddTicks(-1); // 23:59:59.9999999

                // Count records within the date range
                int count = 0;
                const string dateFormat = "yyyy-MM-dd HH:mm:ss";

                // Skip header line (index 0) and process data lines
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    // Split by comma (assuming URL doesn't contain commas)
                    string[] parts = line.Split(',');
                    if (parts.Length < 2)
                        continue;

                    try
                    {
                        // Parse the timestamp from the first column
                        string timestampStr = parts[0].Trim();
                        DateTime recordTimestamp = DateTime.ParseExact(timestampStr, dateFormat, null);

                        // Check if the record is within the date range
                        if (recordTimestamp >= startDate && recordTimestamp <= endDate)
                        {
                            count++;
                        }
                    }
                    catch (FormatException)
                    {
                        // Skip lines with invalid timestamp format
                        continue;
                    }
                }

                // Display the result
                lblResult.Text = $"Total sharings in range: {count}";
                lblStatus.Text = $"Calculation complete. Found {count} record(s) in the selected date range.";
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error calculating count: {ex.Message}";
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = errorMessage;
                lblResult.Text = "Total sharings: --";
            }
        }
    }
}

