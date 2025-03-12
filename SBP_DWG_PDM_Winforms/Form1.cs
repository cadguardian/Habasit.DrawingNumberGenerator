using DNG.Library.Models.Base;
using DNG.Library.Services;
using DNG.Library.Services.Base; // Ensure you have this using directive

namespace SBP_DWG_PDM_Winforms
{
    public partial class Form1 : Form
    {
        private readonly IDrawingNumberDecipherService _decipherService;
        private readonly IDrawingFileService _drawingFileService;
        private List<FileItem> allFiles = new();

        public Form1(IDrawingNumberDecipherService decipherService, IDrawingFileService drawingFileService, HttpClient httpClient)
        {
            _decipherService = decipherService;
            _drawingFileService = drawingFileService;
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            allFiles = await _drawingFileService.LoadAllFilesAsync("data/pdf_drawings.json");
            await LoadDrawingCodes();
        }

        private async Task LoadDrawingCodes()
        {
            try
            {
                // Extract dwgs from sqlite db
                List<string> dwgs = new();
                List<Dictionary<string, (string DrawingCode, string DrawingRequestValue)>>? drawings = new();

                // Transform dwgs to drawings
                int i = 0;
                foreach (var dwg in allFiles)
                {
                    // decypher dwg #
                    var drawing = _decipherService.DecipherDrawingNumber(dwg.Filename);
                    drawings.Add(drawing);
                }

                // Load drawings into memory
                DrawingsGridView.DataSource = drawings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string drawingNumberInput = string.Empty;
        private Dictionary<string, (string DrawingCode, string DrawingRequestValue)> decipheredResult;
        private bool isDeciphered = false;

        private void HandleDecipher()
        {
            if (!string.IsNullOrWhiteSpace(drawingNumberInput))
            {
                decipheredResult = _decipherService.DecipherDrawingNumber(drawingNumberInput);
                isDeciphered = true;
            }
        }

        private void ClearInput()
        {
            drawingNumberInput = string.Empty;
            decipheredResult = null;
            isDeciphered = false;
        }
    }
}