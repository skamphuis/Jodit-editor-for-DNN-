using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DotNetNuke.Common;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Localization;

namespace DNNConnect.CKE
{
    /// <summary>
    /// Jodit HTML Editor Provider for DNN
    /// </summary>
    /// <remarks>
    /// This provider creates a simple TextBox that is then enhanced with Jodit editor via JavaScript.
    /// It doesn't require a specific base class, making it compatible with any DNN version.
    /// The web.config will tell DNN to use this as the HTML editor provider.
    /// </remarks>
    public class JoditHtmlEditorProvider : Control
    {
        private const string ProviderType = "jodit";
        private TextBox _editor;
        private Unit _height = Unit.Parse("400px");
        private Unit _width = Unit.Parse("100%");
        private string _text = string.Empty;

        #region Public Properties

        /// <summary>
        /// Gets or sets the control ID
        /// </summary>
        public string ControlID { get; set; }

        /// <summary>
        /// Gets or sets the width of the editor
        /// </summary>
        public Unit Width
        {
            get => _width;
            set => _width = value;
        }

        /// <summary>
        /// Gets or sets the height of the editor
        /// </summary>
        public Unit Height
        {
            get => _height;
            set => _height = value;
        }

        /// <summary>
        /// Gets or sets the root image directory
        /// </summary>
        public string RootImageDirectory { get; set; }

        /// <summary>
        /// Gets or sets the text/HTML content
        /// </summary>
        public string Text
        {
            get
            {
                EnsureChildControls();
                return _editor?.Text ?? _text;
            }
            set
            {
                _text = value;
                if (_editor != null)
                {
                    _editor.Text = value;
                }
            }
        }

        /// <summary>
        /// Gets the HTML editor control
        /// </summary>
        public Control HtmlEditorControl
        {
            get
            {
                EnsureChildControls();
                return _editor;
            }
        }

        /// <summary>
        /// Gets or sets additional toolbars (for compatibility)
        /// </summary>
        public ArrayList AdditionalToolbars { get; set; } = new ArrayList();

        private PortalSettings PortalSettings => PortalController.Instance.GetCurrentPortalSettings();

        #endregion

        #region Lifecycle Methods

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Create the textarea that will be converted to Jodit editor
            _editor = new TextBox
            {
                ID = string.IsNullOrEmpty(ControlID) ? "joditEditor" : ControlID,
                TextMode = TextBoxMode.MultiLine,
                Text = _text,
                CssClass = "jodit-editor"
            };

            if (!Width.IsEmpty)
            {
                _editor.Width = Width;
            }

            if (!Height.IsEmpty)
            {
                _editor.Height = Height;
            }

            Controls.Add(_editor);
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            EnsureChildControls();
            RegisterClientScripts();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            RegisterJoditInitialization();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the provider (for compatibility)
        /// </summary>
        public void Initialize()
        {
            EnsureChildControls();
        }

        /// <summary>
        /// Add toolbar (for compatibility)
        /// </summary>
        public void AddToolbar()
        {
            // Jodit toolbar is configured via JavaScript config
        }

        #endregion

        #region Private Methods

        private void RegisterClientScripts()
        {
            if (Page == null) return;

            var providerPath = ResolveUrl($"~/Providers/HtmlEditorProviders/{ProviderType}/");

            // Register Jodit CSS
            var cssLink = new HtmlLink
            {
                Href = $"{providerPath}css/jodit.min.css"
            };
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("type", "text/css");

            // Check if header exists before adding
            if (Page.Header != null && !IsStyleSheetRegistered(cssLink.Href))
            {
                Page.Header.Controls.Add(cssLink);
            }

            // Register Jodit JavaScript
            var scriptKey = "JoditLibrary";
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(scriptKey))
            {
                Page.ClientScript.RegisterClientScriptInclude(
                    scriptKey,
                    $"{providerPath}js/jodit.min.js"
                );
            }
        }

        private bool IsStyleSheetRegistered(string href)
        {
            if (Page.Header == null) return false;

            foreach (Control control in Page.Header.Controls)
            {
                if (control is HtmlLink link && link.Href == href)
                {
                    return true;
                }
            }
            return false;
        }

        private void RegisterJoditInitialization()
        {
            if (Page == null || _editor == null) return;

            var config = GetJoditConfiguration();
            var editorId = _editor.ClientID;

            var script = $@"
(function() {{
    if (typeof Jodit === 'undefined') {{
        console.error('Jodit library not loaded');
        return;
    }}
    
    // Wait for DOM to be ready
    if (document.readyState === 'loading') {{
        document.addEventListener('DOMContentLoaded', initJodit);
    }} else {{
        initJodit();
    }}
    
    function initJodit() {{
        var editor = document.getElementById('{editorId}');
        if (editor && !editor.joditInstance) {{
            try {{
                var joditInstance = Jodit.make(editor, {config});
                editor.joditInstance = joditInstance;
                
                // Sync content back to textarea on change
                joditInstance.events.on('change', function() {{
                    editor.value = joditInstance.value;
                }});
                
                // Sync on form submit
                var form = editor.form;
                if (form) {{
                    form.addEventListener('submit', function() {{
                        editor.value = joditInstance.value;
                    }});
                }}
            }} catch(err) {{
                console.error('Error initializing Jodit:', err);
            }}
        }}
    }}
}})();
";

            Page.ClientScript.RegisterStartupScript(
                GetType(),
                $"JoditInit_{editorId}",
                script,
                true
            );
        }

        private string GetJoditConfiguration()
        {
            var providerPath = ResolveUrl($"~/Providers/HtmlEditorProviders/{ProviderType}/");
            var imageUploadUrl = ResolveUrl($"{providerPath}ImageUpload.ashx");
            var fileBrowserUrl = ResolveUrl($"{providerPath}FileBrowser.ashx");

            var height = Height.IsEmpty ? "400" : ((int)Height.Value).ToString();
            var width = Width.IsEmpty ? "'100%'" : $"'{(int)Width.Value}px'";

            // Basic Jodit configuration
            var config = $@"{{
                height: {height},
                width: {width},
                toolbarAdaptive: false,
                buttons: [
                    'source', '|',
                    'bold', 'italic', 'underline', 'strikethrough', '|',
                    'ul', 'ol', '|',
                    'outdent', 'indent', '|',
                    'font', 'fontsize', 'brush', 'paragraph', '|',
                    'image', 'video', 'table', 'link', '|',
                    'align', 'undo', 'redo', '|',
                    'hr', 'eraser', 'copyformat', '|',
                    'symbol', 'fullsize', 'print'
                ],
                uploader: {{
                    url: '{imageUploadUrl}',
                    insertImageAsBase64URI: false
                }},
                filebrowser: {{
                    ajax: {{
                        url: '{fileBrowserUrl}'
                    }}
                }},
                language: '{GetEditorLanguage()}',
                defaultMode: Jodit.MODE_WYSIWYG,
                enter: 'P',
                toolbarButtonSize: 'middle',
                theme: 'default',
                saveModeInStorage: true,
                spellcheck: true,
                editorCssClass: 'jodit-dnn-editor',
                beautifyHTML: true,
                useSearch: true,
                tabIndex: -1
            }}";

            return config;
        }

        private string GetEditorLanguage()
        {
            try
            {
                var cultureName = Localization.GetPageLocale(PortalSettings)?.Name ?? "en";

                // Jodit uses 2-letter codes (en, de, fr, etc.)
                if (cultureName.Contains("-"))
                {
                    cultureName = cultureName.Split('-')[0];
                }

                return cultureName.ToLower();
            }
            catch
            {
                return "en";
            }
        }

        #endregion
    }
}