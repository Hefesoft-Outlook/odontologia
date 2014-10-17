using System.Windows;
using System.Windows.Media.Effects;

namespace ShaderEffectsLibrary
{
    public class WaveEffect : ShaderEffectBase
    {
        private static PixelShader pixelShader = new PixelShader();

        static WaveEffect()
        {
            pixelShader.UriSource = Global.MakePackUri( "Source/WaveEffect.ps" );
        }

        public WaveEffect()
        {
            this.PixelShader = pixelShader;
            this.UpdateShaderValue( WavinessProperty );
        }

        public double Waviness
        {
            get
            {
                return ( double )GetValue( WavinessProperty );
            }
            set
            {
                SetValue( WavinessProperty, value );
            }
        }

        public static readonly DependencyProperty WavinessProperty = 
            DependencyProperty.Register( "Waviness", typeof( double ), typeof( WaveEffect ), 
                new PropertyMetadata( 0.0, PixelShaderConstantCallback( 0 ) ) );
    }
}
