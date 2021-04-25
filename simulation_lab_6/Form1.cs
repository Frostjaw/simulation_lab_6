using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simulation_lab_6
{
    public partial class Form1 : Form
    {
        private bool _isStoppingRequested;
        private bool _isSimulationRunning;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && !char.IsControl(e.KeyChar) && number != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && !char.IsControl(e.KeyChar) && number != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!char.IsDigit(number) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (_isSimulationRunning)
            {
                MessageBox.Show("Simulation already running!");

                return;
            }

            var customerArrivalParameterString = textBox1.Text;
            if (string.IsNullOrEmpty(customerArrivalParameterString))
            {
                MessageBox.Show("Enter parameter for customers arrival");

                return;
            }

            if (!double.TryParse(customerArrivalParameterString, NumberStyles.Any, CultureInfo.InvariantCulture, out var customerArrivalParameter)
                || customerArrivalParameterString == "0")
            {
                MessageBox.Show("Enter valid parameter for customers arrival");

                return;
            }

            var serviceTimeParameterString = textBox2.Text;
            if (string.IsNullOrEmpty(serviceTimeParameterString))
            {
                MessageBox.Show("Enter parameter for service time");

                return;
            }

            if (!double.TryParse(serviceTimeParameterString, NumberStyles.Any, CultureInfo.InvariantCulture, out var serviceTimeParameter)
                || serviceTimeParameterString == "0")
            {
                MessageBox.Show("Enter valid parameter for service time");

                return;
            }

            var operatorsCountString = textBox6.Text;
            if (string.IsNullOrEmpty(operatorsCountString))
            {
                MessageBox.Show("Enter number of operators");

                return;
            }

            if (!uint.TryParse(operatorsCountString, out var operatorsCount)
                || operatorsCountString == "0")
            {
                MessageBox.Show("Enter valid number of operators");

                return;
            }

            _isStoppingRequested = false;

            var bank = new Bank(serviceTimeParameter, operatorsCount);
            var customers = new Customers(customerArrivalParameter, bank);

            var agents = new IAgent[]
            {
                bank,
                customers
            };

            var currentTime = 0d;

            // asynchronous execution for preventing UI blocking
            await Task.Run(async () =>
            {
                _isSimulationRunning = true;

                while (!_isStoppingRequested)
                {
                    var minNextEventTime = double.PositiveInfinity;
                    var nextEventAgentIndex = 0;
                    for (var i = 0; i < agents.Length; i++)
                    {
                        var nextEventTime = agents[i].GetNextEventTime();

                        if (nextEventTime < minNextEventTime)
                        {
                            minNextEventTime = nextEventTime;
                            nextEventAgentIndex = i;
                        }
                    }

                    currentTime += minNextEventTime;
                    agents[nextEventAgentIndex].ProcessEvent();

                    ShowSimulationState(currentTime, bank.QueueLength, bank.ServedPeopleCount);

                    await Task.Delay(TimeSpan.FromSeconds(1));
                }

                _isSimulationRunning = false;
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _isStoppingRequested = true;
        }

        private void ShowSimulationState(double currentTime, uint queueLength, uint servedPeopleCount)
        {
            BeginInvoke((Action)(() =>
            {
                textBox3.Text = currentTime.ToString("f");
                textBox4.Text = queueLength.ToString();
                textBox5.Text = servedPeopleCount.ToString();
            }));
        }
    }
}
