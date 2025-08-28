using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using System.Windows.Forms;
using System.Windows.Media;
using LibreHW = LibreHardwareMonitor.Hardware;
using OpenHW = OpenHardwareMonitor.Hardware;
using System.CodeDom;

namespace Hardware_Monitor
{

    // Main initialization and setup of the form
    public partial class Form1 : Form
    {
        private LiveCharts.WinForms.CartesianChart cpuChart;
        private LiveCharts.WinForms.CartesianChart ramChart;
        private LiveCharts.WinForms.CartesianChart gpuChart;
        private LiveCharts.WinForms.CartesianChart vramChart;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private Timer updateTimer;
        private Timer cpuCoreUpdateTimer;
        private TextBox cpuTempTextBox;
        private TextBox gpuTempTextBox;
        private TextBox gpuHotSpotTempTextBox;
        private LibreHW.Computer computer;
        private OpenHW.Computer openComputer;
        private Label hoverInfoLabel;
        private bool isHardwareInitialized = false;

        private Dictionary<string, TextBox> coreLoadTextBoxes;

        public Form1()
        {
            try
            {
                InitializeComponent();
                InitializeOpenHardwareMonitor();

                // Check if hardware is initialized
                if (computer != null && computer.Hardware.Any())
                {
                    isHardwareInitialized = true;
                }

                InitializeOpenHardwareMonitorForOpenHW();
                this.Size = new Size(2400, 1200);
                this.StartPosition = FormStartPosition.CenterScreen;
                InitializeCharts();
                cpuChart.Background = new SolidColorBrush(Colors.DarkGray);
                ramChart.Background = new SolidColorBrush(Colors.DarkGray);
                gpuChart.Background = new SolidColorBrush(Colors.DarkGray);
                vramChart.Background = new SolidColorBrush(Colors.DarkGray);
                InitializePerformanceCounters();
                InitializeTimer();
                InitializeCpuCoreUpdateTimer();
                AddCpuCoreLoadTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting application: {ex.Message}",
                              "Startup failure",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        // Initialize LibreHardwareMonitor and handle potential errors
        private void InitializeOpenHardwareMonitor()
        {
            try
            {
                // Enable monitoring for CPU, GPU, Motherboard, Memory
                computer = new LibreHW.Computer
                {
                    IsCpuEnabled = true,
                    IsGpuEnabled = true,
                    IsMotherboardEnabled = true,
                    IsMemoryEnabled = true,
                    IsStorageEnabled = false,
                    IsNetworkEnabled = false,
                    IsControllerEnabled = false
                };

                try
                {
                    computer.Open();
                    foreach (var hardware in computer.Hardware)
                    {
                        try
                        {
                            hardware.Update();
                            Console.WriteLine($"Hardware found: {hardware.Name} ({hardware.HardwareType})");

                            // Detailed sensor information
                            foreach (var sensor in hardware.Sensors)
                            {
                                Console.WriteLine($"  Sensor: {sensor.Name}, Type: {sensor.SensorType}, Value: {sensor.Value}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error refreshing hardware {hardware.Name}: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening hardware monitoring: {ex.Message}");
                    MessageBox.Show("Error initializing hardware monitoring. Please run this program as administrator.",
                                  "Initialization error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error during hardware initialization: {ex.Message}");
                MessageBox.Show("Critical error during hardware initialization. Program will exit.",
                              "Critical error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void InitializeOpenHardwareMonitorForOpenHW()
        {
            openComputer = new OpenHW.Computer
            {
                CPUEnabled = true, // Activate CPU monitoring
                GPUEnabled = true  // Activate GPU monitoring
            };
            openComputer.Open(); // Opening the computer for monitoring
        }

        private double GetGpuUsage()
        {
            // Try to get GPU usage with LibreHardwareMonitor first
            double usage = GetGpuUsageFromLibreHardwareMonitor();
            if (usage == 0)
            {
                // Fallback: Try to get GPU usage with OpenHardwareMonitor
                usage = GetGpuUsageFromOpenHardwareMonitor();
            }
            if (usage == 0)
            {
                // Fallback: Try to get GPU usage with WMI
                usage = GetGpuUsageFromWmi();
            }
            return usage;
        }

        private double GetGpuUsageFromLibreHardwareMonitor()
        {
            foreach (LibreHW.IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == LibreHW.HardwareType.GpuNvidia || hardware.HardwareType == LibreHW.HardwareType.GpuAmd)
                {
                    hardware.Update();
                    var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Load);
                    return loadSensor?.Value ?? 0;
                }
            }
            return 0;
        }

        private double GetGpuUsageFromOpenHardwareMonitor()
        {
            try
            {
                foreach (OpenHW.IHardware hardware in openComputer.Hardware)
                {
                    if (hardware.HardwareType == OpenHW.HardwareType.GpuNvidia || hardware.HardwareType == OpenHW.HardwareType.GpuAti)
                    {
                        hardware.Update();
                        var loadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == OpenHW.SensorType.Load);
                        return loadSensor?.Value ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OpenHardwareMonitor: {ex.Message}");
            }

            return 0; // Return 0 if no data found
        }

        private double GetGpuUsageFromWmi()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PerfFormattedData_GPUPerformanceCounters_GPUAdapter");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // GPU usage from WMI
                    return Convert.ToDouble(queryObj["UtilizationPercentage"]);
                }
            }
            catch
            {
                // Ignore errors and return 0
            }
            return 0;
        }

        private double GetVramUsage()
        {
            double usage = GetVramUsageFromLibreHardwareMonitor();
            if (usage == 0)
            {
                usage = GetVramUsageFromOpenHardwareMonitor(); // Fallback-Method
            }
            return usage;
        }

        private double GetVramUsageFromLibreHardwareMonitor()
        {
            foreach (LibreHW.IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == LibreHW.HardwareType.GpuNvidia || hardware.HardwareType == LibreHW.HardwareType.GpuAmd)
                {
                    hardware.Update();
                    var vramLoadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Load && s.Name.Contains("Memory"));
                    return vramLoadSensor?.Value ?? 0;
                }
            }
            return 0;
        }

        private double GetVramUsageFromOpenHardwareMonitor()
        {
            try
            {
                foreach (OpenHW.IHardware hardware in openComputer.Hardware)
                {
                    if (hardware.HardwareType == OpenHW.HardwareType.GpuNvidia || hardware.HardwareType == OpenHW.HardwareType.GpuAti)
                    {
                        hardware.Update();
                        var vramLoadSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == OpenHW.SensorType.Load && s.Name.Contains("Memory"));
                        return vramLoadSensor?.Value ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OpenHardwareMonitor (VRAM): {ex.Message}");
            }

            return 0; // Return 0 if no data found
        }

        private double GetGpuTemperature()
        {
            // Try to get the temperature with LibreHardwareMonitor first
            double temperature = GetGpuTemperatureFromLibreHardwareMonitor();
            if (temperature == 0)
            {
                // Fallback: Try to get the temperature with OpenHardwareMonitor
                temperature = GetGpuTemperatureFromOpenHardwareMonitor();
            }
            if (temperature == 0)
            {
                // Fallback: Try to get the temperature with WMI
                temperature = GetGpuTemperatureFromWmi();
            }
            return temperature;
        }

        private double GetGpuTemperatureFromLibreHardwareMonitor()
        {
            foreach (LibreHW.IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == LibreHW.HardwareType.GpuNvidia || hardware.HardwareType == LibreHW.HardwareType.GpuAmd)
                {
                    hardware.Update();
                    var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Temperature);
                    return tempSensor?.Value ?? 0;
                }
            }
            return 0;
        }

        private double GetGpuTemperatureFromOpenHardwareMonitor()
        {
            try
            {
                foreach (OpenHW.IHardware hardware in openComputer.Hardware)
                {
                    if (hardware.HardwareType == OpenHW.HardwareType.GpuNvidia || hardware.HardwareType == OpenHW.HardwareType.GpuAti)
                    {
                        hardware.Update();
                        var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == OpenHW.SensorType.Temperature);
                        return tempSensor?.Value ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OpenHardwareMonitor (GPU-Temperature): {ex.Message}");
            }

            return 0; // Return 0 if no data found
        }

        private double GetGpuTemperatureFromWmi()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // Get GPU temperature from WMI if available
                    if (queryObj["AdapterCompatibility"] != null && queryObj["AdapterCompatibility"].ToString().Contains("Intel"))
                    {
                        // Here you would implement Intel GPU temperature retrieval if available
                        return 0; // Placeholder for Intel GPUs
                    }
                }
            }
            catch
            {
                // Ignore errors and return 0
            }
            return 0;
        }

        private float GetGpuHotSpotTemperature()
        {
            foreach (LibreHW.IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == LibreHW.HardwareType.GpuNvidia || hardware.HardwareType == LibreHW.HardwareType.GpuAmd)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        Console.WriteLine($"GPU Sensor: {sensor.Name}, Type: {sensor.SensorType}, Value: {sensor.Value}");
                    }

                    var hotspotTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Temperature && s.Name.Contains("Hot Spot"));
                    if (hotspotTempSensor != null)
                    {
                        return hotspotTempSensor.Value ?? 0;
                    }

                    // Fallback: Generic temperature sensor if no Hotspot sensor is found
                    var generalTempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Temperature);
                    if (generalTempSensor != null)
                    {
                        return generalTempSensor.Value ?? 0;
                    }
                }
            }
            return 0; // If no data found, return 0
        }

        private double GetCpuTemperature()
        {
            // Try to get the temperature with LibreHardwareMonitor first
            double temperature = GetCpuTemperatureFromLibreHardwareMonitor();
            if (temperature == 0)
            {
                // Fallback: Try to get the temperature with OpenHardwareMonitor
                temperature = GetCpuTemperatureFromOpenHardwareMonitor();
            }
            if (temperature == 0)
            {
                // Fallback: Try to get the temperature with WMI
                temperature = GetCpuTemperatureFromWmi();
            }
            return temperature;
        }

        private double GetCpuTemperatureFromLibreHardwareMonitor()
        {
            foreach (LibreHW.IHardware hardware in computer.Hardware)
            {
                if (hardware.HardwareType == LibreHW.HardwareType.Cpu)
                {
                    hardware.Update();
                    Console.WriteLine($"Checking CPU sensors for: {hardware.Name}");

                    // Iterate through all temperature sensors
                    foreach (var sensor in hardware.Sensors.Where(s => s.SensorType == LibreHW.SensorType.Temperature))
                    {
                        Console.WriteLine($"Found temperature sensor: {sensor.Name}, Value: {sensor.Value}");

                        // Check for common CPU temperature sensor names
                        if (sensor.Name.Contains("Tctl/Tdie") ||
                            sensor.Name.Contains("Core") ||
                            sensor.Name.Contains("CPU Package"))
                        {
                            return sensor.Value ?? 0;
                        }
                    }

                    // If no specific sensor found, return the first temperature sensor value as a fallback
                    var defaultSensor = hardware.Sensors
                        .FirstOrDefault(s => s.SensorType == LibreHW.SensorType.Temperature);

                    if (defaultSensor != null)
                    {
                        return defaultSensor.Value ?? 0;
                    }
                }
            }
            return 0;
        }

        private double GetCpuTemperatureFromOpenHardwareMonitor()
        {
            try
            {
                foreach (OpenHW.IHardware hardware in openComputer.Hardware)
                {
                    if (hardware.HardwareType == OpenHW.HardwareType.CPU)
                    {
                        hardware.Update();
                        var tempSensor = hardware.Sensors.FirstOrDefault(s => s.SensorType == OpenHW.SensorType.Temperature);
                        return tempSensor?.Value ?? 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in OpenHardwareMonitor (CPU-Temperature): {ex.Message}");
            }

            return 0; // Return 0 if no data found
        }

        private double GetCpuTemperatureFromWmi()
        {
            try
            {
                var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_TemperatureProbe");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // WMI returns temperature in Kelvin, convert to Celsius
                    return Convert.ToDouble(queryObj["CurrentTemperature"]) - 273.15;
                }
            }
            catch
            {
                // Ignore errors and return 0
            }
            return 0;
        }

        private void InitializeCharts()
        {
            // CPU Usage Chart
            cpuChart = new LiveCharts.WinForms.CartesianChart
            {
                Width = this.Width,
                Height = 200,
                Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "CPU Usage",
                Values = new ChartValues<double> { 0 },
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                Stroke = System.Windows.Media.Brushes.Green,
                Fill = System.Windows.Media.Brushes.Transparent
            }
        },
                AxisY = {
            new Axis
            {
                Title = "CPU Usage (%)",
                MinValue = 0,
                MaxValue = 100,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        },
                AxisX = {
            new Axis
            {
                Title = "Time (s)",
                ShowLabels = false,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        }
            };
            cpuChart.Location = new Point(0, 0);
            Controls.Add(cpuChart);

            // RAM Usage Chart
            ramChart = new LiveCharts.WinForms.CartesianChart
            {
                Width = this.Width,
                Height = 200,
                Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "RAM Usage",
                Values = new ChartValues<double> { 0 },
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                Stroke = System.Windows.Media.Brushes.Purple,
                Fill = System.Windows.Media.Brushes.Transparent
            }
        },
                AxisY = {
            new Axis
            {
                Title = "RAM Usage (%)",
                MinValue = 0,
                MaxValue = 100,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        },
                AxisX = {
            new Axis
            {
                Title = "Time (s)",
                ShowLabels = false,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        }
            };
            ramChart.Location = new Point(0, cpuChart.Bottom + 10);
            Controls.Add(ramChart);

            // GPU Usage Chart
            gpuChart = new LiveCharts.WinForms.CartesianChart
            {
                Width = this.Width,
                Height = 200,
                Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "GPU Usage",
                Values = new ChartValues<double> { 0 },
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                Stroke = System.Windows.Media.Brushes.Blue,
                Fill = System.Windows.Media.Brushes.Transparent
            }
        },
                AxisY = {
            new Axis
            {
                Title = "GPU Usage (%)",
                MinValue = 0,
                MaxValue = 100,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        },
                AxisX = {
            new Axis
            {
                Title = "Time (s)",
                ShowLabels = false,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        }
            };
            gpuChart.Location = new Point(0, ramChart.Bottom + 10);
            Controls.Add(gpuChart);

            // VRAM Usage Chart
            vramChart = new LiveCharts.WinForms.CartesianChart
            {
                Width = this.Width,
                Height = 200,
                Series = new SeriesCollection
        {
            new LineSeries
            {
                Title = "VRAM Usage",
                Values = new ChartValues<double> { 0 },
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                Stroke = System.Windows.Media.Brushes.Cyan,
                Fill = System.Windows.Media.Brushes.Transparent
            }
        },
                AxisY = {
            new Axis
            {
                Title = "VRAM Usage (%)",
                MinValue = 0,
                MaxValue = 100,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        },
                AxisX = {
            new Axis
            {
                Title = "Time (s)",
                ShowLabels = false,
                Foreground = System.Windows.Media.Brushes.White,
                FontSize = 14
            }
        }
            };
            vramChart.Location = new Point(0, gpuChart.Bottom + 10);
            Controls.Add(vramChart);

        // CPU Temperature TextBox
        cpuTempTextBox = new TextBox
            {
                Location = new Point(10, vramChart.Bottom + 10),
                Width = 320,
                Font = new Font("Trebuchet MS", 14),
                ReadOnly = true,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.White,
                Text = "CPU Temperature: N/A"
            };

            // GPU Temperature TextBox
            gpuTempTextBox = new TextBox
            {
                Location = new Point(10, vramChart.Bottom + 10),
                Width = 320,
                Font = new Font("Trebuchet MS", 14),
                ReadOnly = true,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.White,
                Text = "GPU Temperature: N/A"
            };

            gpuHotSpotTempTextBox = new TextBox
            {
                Location = new Point(10, vramChart.Bottom + 10),
                Width = 335,
                Font = new Font("Trebuchet MS", 14),
                ReadOnly = true,
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.White,
                Text = "GPU HotSpot Temp: N/A"
            };

            // Center the textboxes
            int textBoxSpacing = 10; // Space between text boxes
            int totalWidth = cpuTempTextBox.Width + gpuTempTextBox.Width + textBoxSpacing;
            int centerX = (this.Width - totalWidth) / 2; // Calculate center position

            // Position CPU Temperature TextBox
            cpuTempTextBox.Location = new Point(centerX - 120, vramChart.Bottom + 10);
            Controls.Add(cpuTempTextBox);

            // Position GPU Temperature TextBox
            gpuTempTextBox.Location = new Point(centerX -120 + cpuTempTextBox.Width + textBoxSpacing, vramChart.Bottom + 10);
            Controls.Add(gpuTempTextBox);

            // Position GPU HotSpot Temperature TextBox
            gpuHotSpotTempTextBox.Location = new Point(centerX -120 + cpuTempTextBox.Width + gpuTempTextBox.Width + 2 * textBoxSpacing, vramChart.Bottom + 10);
            Controls.Add(gpuHotSpotTempTextBox);

            hoverInfoLabel = new Label
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = System.Drawing.Color.White,
                Location = new Point(20, this.Height - 40) // Adjust position accordingly
            };
            Controls.Add(hoverInfoLabel);
        }

        private void AddCpuCoreLoadTextBoxes()
        {
            coreLoadTextBoxes = new Dictionary<string, TextBox>(); // Initialisiere das Dictionary
            int xOffset = 10; // Startposition X
            int yOffset = gpuTempTextBox.Bottom + 60; // Startposition Y unterhalb anderer Komponenten
            int textboxWidth = 210;
            int textboxHeight = 30;
            int padding = 10; // Space between text boxes
            int columns = 4; // Number of columns in the grid
            int currentColumn = 0;

            foreach (var hardware in computer.Hardware.Where(h => h.HardwareType == LibreHW.HardwareType.Cpu))
            {
                foreach (var sensor in hardware.Sensors.Where(s => s.SensorType == LibreHW.SensorType.Load && s.Name.Contains("CPU Core")))
                {
                    var coreLoadTextBox = new TextBox
                    {
                        Width = textboxWidth,
                        Height = textboxHeight,
                        Text = $"{sensor.Name}: N/A",
                        ReadOnly = true,
                        BackColor = System.Drawing.Color.Black,
                        ForeColor = System.Drawing.Color.White,
                        Font = new Font("Trebuchet MS", 10)
                    };

                    // Position the TextBox in a grid layout
                    coreLoadTextBox.Location = new Point(xOffset + (currentColumn * (textboxWidth + padding)), yOffset);
                    Controls.Add(coreLoadTextBox);
                    coreLoadTextBoxes[sensor.Name] = coreLoadTextBox; // Save to dictionary

                    currentColumn++;

                    // Move to next row if current row is filled
                    if (currentColumn >= columns)
                    {
                        currentColumn = 0;
                        yOffset += textboxHeight + padding;
                    }
                }
            }
        }


        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        private void InitializeTimer()
        {
            updateTimer = new Timer { Interval = 2000 }; // Update all 2000 ms
            updateTimer.Tick += UpdateCharts;
            updateTimer.Start();
        }

        private void InitializeCpuCoreUpdateTimer()
        {
            cpuCoreUpdateTimer = new Timer { Interval = 500 }; // Update all 500 ms
            cpuCoreUpdateTimer.Tick += (sender, e) => UpdateCpuCoreLoadTextBoxes();
            cpuCoreUpdateTimer.Start();
        }

        private async void UpdateCharts(object sender, EventArgs e)
        {
            try
            {
                // Asyncronous data retrieval
                var cpuUsageTask = Task.Run(() => cpuCounter.NextValue());
                var totalRamTask = Task.Run(() => GetTotalMemoryInMB());
                var availableRamTask = Task.Run(() => ramCounter.NextValue());
                var gpuUsageTask = Task.Run(() => GetGpuUsage());
                var vramUsageTask = Task.Run(() => GetVramUsage());
                var cpuTempTask = Task.Run(() => GetCpuTemperature());
                var gpuTempTask = Task.Run(() => GetGpuTemperature());
                var gpuHotSpotTempTask = Task.Run(() => GetGpuHotSpotTemperature());

                // Wait for all tasks to complete
                await Task.WhenAll(cpuUsageTask, totalRamTask, availableRamTask, gpuUsageTask, vramUsageTask, cpuTempTask, gpuTempTask, gpuHotSpotTempTask);

                // Collect results
                double cpuUsage = cpuUsageTask.Result;
                double totalRam = totalRamTask.Result;
                double availableRam = availableRamTask.Result;
                double ramUsage = 100 * (1 - (availableRam / totalRam));
                double gpuUsage = gpuUsageTask.Result;
                double vramUsage = vramUsageTask.Result;
                double cpuTemp = cpuTempTask.Result;
                double gpuTemp = gpuTempTask.Result;
                float gpuHotSpotTemp = gpuHotSpotTempTask.Result;

                // Update Charts
                UpdateChart(cpuChart, cpuUsage);
                UpdateChart(ramChart, ramUsage);
                UpdateChart(gpuChart, gpuUsage);
                UpdateChart(vramChart, vramUsage);

                // Update TextBoxes
                cpuTempTextBox.Text = $"CPU Temperature: {Math.Round(cpuTemp)} °C";
                gpuTempTextBox.Text = $"GPU Temperature: {gpuTemp} °C";
                gpuHotSpotTempTextBox.Text = $"GPU HotSpot Temp: {Math.Round(gpuHotSpotTemp)} °C";

                // Update CPU Core Load TextBoxes
                UpdateCpuCoreLoadTextBoxes();
            }
            catch (Exception ex)
            {
                // Error handling
                Console.WriteLine($"Error updating Charts: {ex.Message}");
            }
        }

        private void UpdateCpuCoreLoadTextBoxes()
        {
            if (!isHardwareInitialized) return;

            try
            {
                var cpuHardware = computer.Hardware.FirstOrDefault(h => h.HardwareType == LibreHW.HardwareType.Cpu);
                if (cpuHardware != null)
                {
                    cpuHardware.Update();
                    foreach (var sensor in cpuHardware.Sensors.Where(s => s.SensorType == LibreHW.SensorType.Load && s.Name.Contains("CPU Core")))
                    {
                        if (coreLoadTextBoxes.TryGetValue(sensor.Name, out var textBox))
                        {
                            var newValue = $"{sensor.Name}: {Math.Round(sensor.Value ?? 0, 2)} %";
                            if (textBox.Text != newValue)
                            {
                                textBox.Text = newValue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating CPU-Core loads: {ex.Message}");
            }
        }

        private void UpdateChart(LiveCharts.WinForms.CartesianChart chart, double value)
        {
            var values = ((LineSeries)chart.Series[0]).Values;
            if (values.Count > 30) values.RemoveAt(0); // View last 30 entries
            values.Add(value);
        }

        private double GetTotalMemoryInMB()
        {
            var searcher = new ManagementObjectSearcher("Select * From Win32_ComputerSystem");
            foreach (var item in searcher.Get())
            {
                return Math.Round(Convert.ToDouble(item["TotalPhysicalMemory"]) / (1024 * 1024), 2);
            }
            return 0;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            computer.Close();
            openComputer?.Close();
            base.OnFormClosing(e);
        }
    }
}