

namespace LinkeD365.OrgSettings
{
    public class BeforeRenderingEventArgs
    {
        public ToggleSwitchRendererBase Renderer { get; set; }

        public BeforeRenderingEventArgs(ToggleSwitchRendererBase renderer)
        {
            Renderer = renderer;
        }
    }
}
