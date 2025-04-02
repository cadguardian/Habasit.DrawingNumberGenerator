namespace EmailParserDesktop
{
    public partial class DCMForm : Form
    {
        public DCMForm()
        {
            InitializeComponent();
        }

        public EmailParser emailParser = EmailParser.CreateEmailParser();

        private void DCMForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emailBody = textBox1.Text;

            if (emailBody == "") return;

            string? drawingNumber = emailParser.ParseEmail(emailBody)?.DrawingNumber;

            textBox2.Text = drawingNumber;
        }
    }
}