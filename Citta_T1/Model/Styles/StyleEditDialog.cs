using System.ComponentModel;
using C2.Controls;
using C2.Globalization;

namespace C2.Model.Styles
{
    class StyleEditDialog : PropertyDialog
    {
        private Style _Style;

        public StyleEditDialog()
        {
            Style = new TopicStyle();
        }

        [DefaultValue(null)]
        public Style Style
        {
            get { return _Style; }
            set 
            {
                if (_Style != value)
                {
                    _Style = value;
                    OnStyleChanged();
                }
            }
        }

        private void OnStyleChanged()
        {
            this.SelectedObject = Style;
        }

        protected override void OnCurrentLanguageChanged()
        {
            base.OnCurrentLanguageChanged();

            Text = Lang._("Style");
        }
    }
}
