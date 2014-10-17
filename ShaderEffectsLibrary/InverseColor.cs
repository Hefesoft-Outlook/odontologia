using System.Windows.Media.Effects;

namespace ShaderEffectsLibrary
{
    public class InverseColor : ShaderEffectBase
    {
        private static PixelShader pixelShader = new PixelShader();

        static InverseColor()
        {
            pixelShader.UriSource = Global.MakePackUri( "Source/InverseColor.ps" );
        }

        public InverseColor()
        {
            this.PixelShader = pixelShader;
        }
    }
}
