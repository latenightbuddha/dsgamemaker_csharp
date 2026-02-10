using System.Drawing;
using System.Windows.Forms;

namespace DS_Game_Maker
{
    public class clsToolstripRenderer : ToolStripProfessionalRenderer
    {

        // // Render container background gradient
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);

            var b = new System.Drawing.Drawing2D.LinearGradientBrush(e.AffectedBounds, DS_Game_Maker.clsColors.clrVerBG_White, DS_Game_Maker.clsColors.clrVerBG_GrayBlue, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            var shadow = new SolidBrush(DS_Game_Maker.clsColors.clrVerBG_Shadow);
            var rect = new Rectangle(0, e.ToolStrip.Height - 2, e.ToolStrip.Width, 1);
            e.Graphics.FillRectangle(b, e.AffectedBounds);
            e.Graphics.FillRectangle(shadow, rect);
        }

        // // Render button selected and pressed state
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderButtonBackground(e);
            if (e.Item.Selected | ((ToolStripButton)e.Item).Checked)
            {
                var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
                var b = new System.Drawing.Drawing2D.LinearGradientBrush(rect, DS_Game_Maker.clsColors.clrToolstripBtnGrad_White, DS_Game_Maker.clsColors.clrToolstripBtnGrad_Blue, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                var b2 = new SolidBrush(DS_Game_Maker.clsColors.clrToolstripBtn_Border);

                e.Graphics.FillRectangle(b2, rectBorder);
                e.Graphics.FillRectangle(b, rect);
            }
            if (e.Item.Pressed)
            {
                var rectBorder = new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1);
                var rect = new Rectangle(1, 1, e.Item.Width - 2, e.Item.Height - 2);
                var b = new System.Drawing.Drawing2D.LinearGradientBrush(rect, DS_Game_Maker.clsColors.clrToolstripBtnGrad_White_Pressed, DS_Game_Maker.clsColors.clrToolstripBtnGrad_Blue_Pressed, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                var b2 = new SolidBrush(DS_Game_Maker.clsColors.clrToolstripBtn_Border);

                e.Graphics.FillRectangle(b2, rectBorder);
                e.Graphics.FillRectangle(b, rect);
            }
        }



    }
}