using System;
using System.Runtime.InteropServices;
using NetX.Interop;
using NetX.Interop.Internal;
using NetX.XWindows.Events;

namespace NetX.XWindows.Internal
{
    internal sealed class EventEngine
    {
        private IntPtr systemEvent;
        private XApplication application;

        private XWindow window;

        internal EventEngine(XApplication application)
        {
            this.application = application;
        }

        internal void NonBlockingEventLoop()
        {
            do
            {
                systemEvent = LibXcb.xcb_wait_for_event(application.Connection);

                if (systemEvent == IntPtr.Zero) break;

                var evnt = Marshal.PtrToStructure<XcbGenericEvent>(systemEvent);
                switch(evnt.response_type & ~0x80)
                {
                    case (int)XcbEventType.XCB_EXPOSE :
                        var xee = Marshal.PtrToStructure<XcbExposeEvent>(systemEvent);
                        window.OnWindowExposed(new ExposeEventArgs(xee.width,xee.height));
                        application.Flush();
                    break;

                    case (int)XcbEventType.XCB_BUTTON_PRESS :
                        var xbp = Marshal.PtrToStructure<XcbButtonPressReleaseEvent>(systemEvent);
                        Console.WriteLine($"Mouse Key State {xbp.state}");
                    break;

                    case (int)XcbEventType.XCB_KEY_PRESS :
                        var xkp = Marshal.PtrToStructure<XcbKeyPressReleaseEvent>(systemEvent);
                        Console.WriteLine($"Keyboard Key Detail {xkp.detail}");
                    break;

                    case (int)XcbEventType.XCB_CLIENT_MESSAGE :
                        var xcme = Marshal.PtrToStructure<XcbClientMessageEvent>(systemEvent);
                        if(xcme.data.data32[0] == application.quitToken)
                            application.OnApplicationTerminated();
                    break;

                    case (int)XcbEventType.XCB_PROPERTY_NOTIFY :
                    /*
                        var xpne = Marshal.PtrToStructure<XcbPropertyNotifyEvent>(systemEvent);
                        if(xpne.atom == application.ReadWMState("_NET_WM_STATE"))
                            {
                                var val = application.GetValueForProperty(xpne.atom);
                                if(val == application.ReadWMState("_NET_WM_STATE_HIDDEN"))
                                    {
                                        Console.WriteLine("window minimized");
                                    }
                                 else
                                    {
                                        if ( val == application.ReadWMState("_NET_WM_STATE_MAXIMIZED_VERT") || 
                                             val == application.ReadWMState("_NET_WM_STATE_MAXIMIZED_HORZ"))
                                            Console.WriteLine("Maximized");
                                    }
                            }
                     */
                    break;
                    
                    default :
                    break;
                    
                }
                // free unmanaged memmory allocated for event
                Marshal.FreeHGlobal(systemEvent);

            } while (systemEvent != IntPtr.Zero);
        }

        internal void Register(XWindow window)
        {
            //todo: implement for other controls
            // Note: In order to register for events your control or window needs to 
            // expose On"EventType"Event method that recieve event args for the required event
            // e.g to recieve expose event, a control or window needs to have OnExposeEvent(ExposeEventArgs args)
            // method as internal or public
            this.window = window;
        }
        
    }
}