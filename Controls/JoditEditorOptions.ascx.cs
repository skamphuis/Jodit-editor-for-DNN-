using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.UI.Modules;
using System;
using System.Web.UI;

namespace DNNConnect.CKE.Controls
{
    /// <summary>
    /// Settings control for Jodit HTML Editor Provider
    /// </summary>
    public partial class JoditEditorOptions : UserControl, ISettingsControl
    {
        #region IModuleControl Members (Required by ISettingsControl)

        /// <summary>
        /// Gets or sets the module context
        /// </summary>
        public ModuleInstanceContext ModuleContext { get; set; }

        /// <summary>
        /// Gets the underlying base control
        /// </summary>
        public Control Control => this;

        /// <summary>
        /// Gets the path to the control
        /// </summary>
        public string ControlPath => TemplateSourceDirectory + "/";

        /// <summary>
        /// Gets the control name
        /// </summary>
        public string ControlName => GetType().Name.Replace("_", ".");

        /// <summary>
        /// Gets or sets the local resource file path
        /// </summary>
        public string LocalResourceFile { get; set; }

        #endregion

        #region ISettingsControl Members

        /// <summary>
        /// Load settings from configuration
        /// </summary>
        public void LoadSettings()
        {
            try
            {
                var settings = GetProviderSettings();

                if (settings != null)
                {
                    // Load editor mode
                    if (settings.ContainsKey("EditorMode"))
                    {
                        ddlEditorMode.SelectedValue = settings["EditorMode"].ToString();
                    }

                    // Load default height
                    if (settings.ContainsKey("DefaultHeight"))
                    {
                        txtDefaultHeight.Text = settings["DefaultHeight"].ToString();
                    }

                    // Load toolbar sticky
                    if (settings.ContainsKey("ToolbarSticky"))
                    {
                        chkToolbarSticky.Checked = Convert.ToBoolean(settings["ToolbarSticky"]);
                    }

                    // Load character counter
                    if (settings.ContainsKey("ShowCharsCounter"))
                    {
                        chkShowCharsCounter.Checked = Convert.ToBoolean(settings["ShowCharsCounter"]);
                    }

                    // Load word counter
                    if (settings.ContainsKey("ShowWordsCounter"))
                    {
                        chkShowWordsCounter.Checked = Convert.ToBoolean(settings["ShowWordsCounter"]);
                    }

                    // Load XPath in statusbar
                    if (settings.ContainsKey("ShowXPathInStatusbar"))
                    {
                        chkShowXPathInStatusbar.Checked = Convert.ToBoolean(settings["ShowXPathInStatusbar"]);
                    }

                    // Load resize option
                    if (settings.ContainsKey("AllowResizeY"))
                    {
                        chkAllowResizeY.Checked = Convert.ToBoolean(settings["AllowResizeY"]);
                    }

                    // Load spellcheck
                    if (settings.ContainsKey("Spellcheck"))
                    {
                        chkSpellcheck.Checked = Convert.ToBoolean(settings["Spellcheck"]);
                    }

                    // Load theme
                    if (settings.ContainsKey("Theme"))
                    {
                        ddlTheme.SelectedValue = settings["Theme"].ToString();
                    }

                    // Load image upload settings
                    if (settings.ContainsKey("ImageUploadEnabled"))
                    {
                        chkImageUploadEnabled.Checked = Convert.ToBoolean(settings["ImageUploadEnabled"]);
                    }

                    if (settings.ContainsKey("MaxImageFileSize"))
                    {
                        txtMaxImageFileSize.Text = settings["MaxImageFileSize"].ToString();
                    }

                    // Load custom config
                    if (settings.ContainsKey("CustomConfig"))
                    {
                        txtCustomConfig.Text = settings["CustomConfig"].ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Save settings to configuration
        /// </summary>
        public void UpdateSettings()
        {
            try
            {
                var settings = new System.Collections.Hashtable
                {
                    ["EditorMode"] = ddlEditorMode.SelectedValue,
                    ["DefaultHeight"] = txtDefaultHeight.Text,
                    ["ToolbarSticky"] = chkToolbarSticky.Checked,
                    ["ShowCharsCounter"] = chkShowCharsCounter.Checked,
                    ["ShowWordsCounter"] = chkShowWordsCounter.Checked,
                    ["ShowXPathInStatusbar"] = chkShowXPathInStatusbar.Checked,
                    ["AllowResizeY"] = chkAllowResizeY.Checked,
                    ["Spellcheck"] = chkSpellcheck.Checked,
                    ["Theme"] = ddlTheme.SelectedValue,
                    ["ImageUploadEnabled"] = chkImageUploadEnabled.Checked,
                    ["MaxImageFileSize"] = txtMaxImageFileSize.Text,
                    ["CustomConfig"] = txtCustomConfig.Text
                };

                SaveProviderSettings(settings);
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion

        #region Private Methods

        private System.Collections.Hashtable GetProviderSettings()
        {
            // In a real implementation, you would load these from DNN's configuration
            // This could be from web.config, database, or a config file
            // For now, return null to use defaults

            // Example implementation would query PortalController or HostController
            // var controller = new DotNetNuke.Entities.Controllers.HostController();
            // var settingsJson = controller.GetString("JoditEditorSettings");
            // return JsonConvert.DeserializeObject<Hashtable>(settingsJson);

            return null;
        }

        private void SaveProviderSettings(System.Collections.Hashtable settings)
        {
            // In a real implementation, you would save these to DNN's configuration
            // This could be to web.config, database, or a config file

            // Example implementation would use PortalController or HostController
            // var controller = new DotNetNuke.Entities.Controllers.HostController();
            // var settingsJson = JsonConvert.SerializeObject(settings);
            // controller.Update("JoditEditorSettings", settingsJson);
        }

        #endregion
    }
}