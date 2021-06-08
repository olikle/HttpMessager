using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HttpMessager
{
    /// <summary>
    /// http://www.gabescode.com/dotnet/2018/11/01/basic-HttpListener-web-service.html
    /// </summary>
    class SimpleHttpListener
    {
        #region variable
        /// <summary>
        /// The port the HttpListener should listen on
        /// </summary>
        private int _port;

        /// <summary>
        /// This is the heart of the web server
        /// </summary>
        private readonly HttpListener _httpListener;

        /// <summary>
        /// A flag to specify when we need to stop
        /// </summary>
        private volatile bool _keepGoing = true;

        /// <summary>
        /// Keep the task in a static variable to keep it alive
        /// </summary>
        private Task _mainLoop;

        #endregion

        #region Constructur
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpServer"/> class.
        /// </summary>
        public SimpleHttpListener(int port)
        {
            _port = port;
            //_httpListener = new HttpListener { Prefixes = { $"http://localhost:{_port}/" } };
            //_httpListener = new HttpListener { Prefixes = { $"http://*:{_port}/" } };
            _httpListener = new HttpListener();

            //_httpListener.Prefixes.Add($"http://localhost:{_port}/");
            //_httpListener.Prefixes.Add($"http://127.0.0.1:{_port}/");
            _httpListener.Prefixes.Add($"http://+:{_port}/"); // you have to add the urlacl - see readme.txt

            IPAddress[] addrs = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            foreach (IPAddress addr in addrs)
            {
                //_httpListener.Prefixes.Add($"http://{addr}:{_port}/");
            }

            //_httpListener.Start();
            //Automatically set the IP address
            //string[] ips = addrs.Select(ip => ip.ToString()).ToArray();
        }
        #endregion

        #region StartWebServer, StopWebServer
        /// <summary>
        /// Starts the web server.
        /// </summary>
        public void StartWebServer()
        {
            OnRecieveStatus(new HttpMessagerEventArgs("status", "Start Webserver"));

            _keepGoing = true;

            if (_mainLoop != null && !_mainLoop.IsCompleted) return; //Already started
            _mainLoop = MainLoop();
        }

        /// <summary>
        /// Call this to stop the web server. It will not kill any requests currently being processed.
        /// </summary>
        public void StopWebServer()
        {
            OnRecieveStatus(new HttpMessagerEventArgs("status", "Stop Webserver"));
            _keepGoing = false;
            lock (_httpListener)
            {
                //Use a lock so we don't kill a request that's currently being processed
                _httpListener.Stop();
            }
            //????
            //try
            //{
            //    _mainLoop.Wait();
            //}
            //catch { /* je ne care pas */ }
        }
        #endregion

        #region MainLoop
        /// <summary>
        /// The main loop to handle requests into the HttpListener
        /// </summary>
        /// <returns></returns>
        private async Task MainLoop()
        {
            _httpListener.Start();
            OnRecieveStatus(new HttpMessagerEventArgs("status", "Mainloop..."));
            while (_keepGoing)
            {
                try
                {
                    //GetContextAsync() returns when a new request come in
                    var context = await _httpListener.GetContextAsync();
                    lock (_httpListener)
                    {
                        if (_keepGoing) ProcessRequest(context);
                    }
                }
                catch (Exception e)
                {
                    if (e is HttpListenerException) return; //this gets thrown when the listener is stopped
                    //TODO: Log the exception
                }
                Application.DoEvents();
            }
        }
        #endregion

        #region ProcessRequest
        /// <summary>
        /// Handle an incoming request
        /// </summary>
        /// <param name="context">The context of the incoming request</param>
        private void ProcessRequest(HttpListenerContext context)
        {
            using (var response = context.Response)
            {
                try
                {
                    var handled = false;
                    //OnRecieveStatus(new HttpMessagerEventArgs("status", $"AbsolutePath: {context.Request.Url.AbsolutePath}"));
                    //OnRecieveStatus(new HttpMessagerEventArgs("status", $"Query {context.Request.Url.Query}"));
                    //OnRecieveStatus(new HttpMessagerEventArgs("status", $"Fragment {context.Request.Url.Fragment}"));
                    OnRecieveStatus(new HttpMessagerEventArgs("status", $"{context.Request.Url.Host}"));
                    OnRecieveStatus(new HttpMessagerEventArgs("status", $"PathAndQuery {context.Request.Url.PathAndQuery}"));
                    switch (context.Request.Url.AbsolutePath)
                    {
                        //This is where we do different things depending on the URL
                        case "/":
                            if (context.Request.HttpMethod == "GET")
                            {
                                CreateResponse(response, "<html><head><title>Simple Http Listener</title><body><h1>Simple Http Listener</h1></body></html>", "text/html");
                                handled = true;
                                break;
                            }
                            break;
                        case "/message":
                            if (context.Request.HttpMethod == "GET")
                            {
                                var message = context.Request.QueryString["text"];
                                if (string.IsNullOrEmpty(message)) message = "no message text set";
                                OnRecieveMessage(new HttpMessagerEventArgs("message", message));
                                CreateResponse(response, "{ \"status\": \"OK\"");
                                handled = true;
                                break;
                            }
                            break;
                        ////TODO: Add cases for each URL we want to respond to
                        //case "/settings":
                        //    switch (context.Request.HttpMethod)
                        //    {
                        //        case "GET":
                        //            //Get the current settings
                        //            response.ContentType = "application/json";

                        //            //This is what we want to send back
                        //            //var responseBody = JsonConvert.SerializeObject(MyApplicationSettings);
                        //            var responseBody = "{ \"success\": \"true\"}";

                        //            //Write it to the response stream
                        //            var buffer = Encoding.UTF8.GetBytes(responseBody);
                        //            response.ContentLength64 = buffer.Length;
                        //            response.OutputStream.Write(buffer, 0, buffer.Length);
                        //            handled = true;
                        //            break;

                        //        case "PUT":
                        //            //Update the settings
                        //            using (var body = context.Request.InputStream)
                        //            using (var reader = new StreamReader(body, context.Request.ContentEncoding))
                        //            {
                        //                //Get the data that was sent to us
                        //                var json = reader.ReadToEnd();

                        //                //Use it to update our settings
                        //                UpdateSettings(JsonConvert.DeserializeObject<MySettings>(json));

                        //                //Return 204 No Content to say we did it successfully
                        //                response.StatusCode = 204;
                        //                handled = true;
                        //            }
                        //            break;
                        //    }
                        //    break;
                    }
                    if (!handled)
                    {
                        response.StatusCode = 404;
                    }
                }
                catch (Exception e)
                {
                    OnRecieveStatus(new HttpMessagerEventArgs("error", $"Error {e.Message}"));
                    //Return the exception details the client - you may or may not want to do this
                    CreateResponse(response, JsonConvert.SerializeObject(e), null, 500);
                    //response.StatusCode = 500;
                    //response.ContentType = "application/json";
                    //var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e));
                    //response.ContentLength64 = buffer.Length;
                    //response.OutputStream.Write(buffer, 0, buffer.Length);

                    //TODO: Log the exception
                }
            }
        }
        #endregion

        #region CreateResponse
        /// <summary>
        /// Creates the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="responseBody">The response body.</param>
        private static void CreateResponse(HttpListenerResponse response, string responseBody, string contentType = null, int? statusCode = null)
        {
            //Set the Status Code
            if (statusCode != null)
                response.StatusCode = (int)statusCode;

            //Set the content Type
            if (contentType != null)
                response.ContentType = contentType;
            else
                response.ContentType = "application/json";

            //Write it to the response stream
            var buffer = Encoding.UTF8.GetBytes(responseBody);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);

        }
        #endregion

        #region Event Handler
        /// <summary>
        /// Occurs when [recieve message].
        /// </summary>
        public event HttpMessagerEventHandler RecieveMessage;
        /// <summary>
        /// Raises the <see cref="E:RecieveMessage" /> event.
        /// </summary>
        /// <param name="e">The <see cref="HttpMessagerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnRecieveMessage(HttpMessagerEventArgs e)
        {
            if (RecieveMessage != null) RecieveMessage(this, e);
        }

        /// <summary>
        /// Occurs when [recieve status].
        /// </summary>
        public event HttpMessagerEventHandler RecieveStatus;
        /// <summary>
        /// Raises the <see cref="E:RecieveStatus" /> event.
        /// </summary>
        /// <param name="e">The <see cref="HttpMessagerEventArgs"/> instance containing the event data.</param>
        protected virtual void OnRecieveStatus(HttpMessagerEventArgs e)
        {
            if (RecieveStatus != null) RecieveStatus(this, e);
        }
        #endregion
    }

    class SimpleHttpListenerX
    {
        #region variable
        private HttpListener listener;
        private Task listenTask;

        public string url = "http://localhost:8000/";
        public int pageViews = 0;
        public int requestCount = 0;
        public string pageData =
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>Http Messager</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";
        #endregion

        #region Constructur
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleHttpServer"/> class.
        /// </summary>
        public SimpleHttpListenerX()
        {
        }
        #endregion

        #region StartListener, StopListener
        /// <summary>
        /// Starts the listener.
        /// </summary>
        public void StartListener()
        {
            // Create a Http server and start listening for incoming connections
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            listenTask = HandleIncomingConnections();
            listenTask.Start();

            //listenTask.GetAwaiter().GetResult();

        }
        /// <summary>
        /// Stops the listener.
        /// </summary>
        public void StopListener()
        {
            if (listener == null)
                return;
            // Close the listener
            listener.Close();
        }
        #endregion


        #region HandleIncomingConnections
        /// <summary>
        /// Handles the incoming connections.
        /// </summary>
        private async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the page
                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Shutdown requested");
                    runServer = false;
                }

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
        #endregion
    }

    #region HttpMessagerEventArgs class
    /// <summary>
    /// ScanItemEventArgs
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class HttpMessagerEventArgs : System.EventArgs
    {
        #region variables
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Message { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessagerEventArgs"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public HttpMessagerEventArgs(string type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
        #endregion
    }
    #endregion

    #region HttpMessagerEventHandler Declaration
    /// <summary>
    /// HttpMessagerEventHandler Declaration
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ScanItemEventArgs"/> instance containing the event data.</param>
    public delegate void HttpMessagerEventHandler(object sender, HttpMessagerEventArgs e);
    #endregion
}
