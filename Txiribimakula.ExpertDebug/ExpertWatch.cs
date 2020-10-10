using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Txiribimakula.ExpertWatch.ViewModels;

namespace Txiribimakula.ExpertWatch
{
    [Guid("6FD34CCE-6A7A-4016-878E-6A639BD79D69")]
    class ExpertWatch : ToolWindowPane
    {
        DebuggerEvents DebuggerEvents;

        public ExpertWatch() : base(null) {
            Caption = "Expert Watch";

            DTE2 DTE2 = ExpertWatchCommand.Instance.ServiceProvider.GetService(typeof(DTE)) as DTE2;

            if(DTE2 != null) {
                ViewModel viewModel = new ViewModel(DTE2.Debugger);

                BlueprintsOptionPage page = (BlueprintsOptionPage)ExpertWatchCommand.Instance.package.GetDialogPage(typeof(BlueprintsOptionPage));
                page.OptionChangedEvent += viewModel.OnToolsOptionsBlueprintsChanged;

                viewModel.OnToolsOptionsBlueprintsChanged(page.Blueprints);

                ExpertWatchWindow window = new ExpertWatchWindow();
                window.DataContext = viewModel;

                Content = window;
                DebuggerEvents = DTE2.Events.DebuggerEvents;
                DebuggerEvents.OnEnterBreakMode += viewModel.OnEnterBreakMode;
                DebuggerEvents.OnEnterRunMode += ExpertWatchCommand.Instance.RunHandler;
                DebuggerEvents.OnEnterDesignMode += ExpertWatchCommand.Instance.DesignHandler;
            }
        }
    }
}
