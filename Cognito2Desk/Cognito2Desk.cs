using System;
using System.Collections.Generic;
using System.Text;
using MinimalisticTelnet;

namespace Cognito2Desk {
    public class Cognito2Desk {
        string PROMPT = "API>";

        public string INTENSITY = "Intensity";
        public string RED_CYC = "Color A";
        public string GREEN_CYC = "Color B";
        public string BLUE_CYC = "Color C";

        private TelnetConnection deskConnection;

        public bool IsConnected {
            get {
                return deskConnection.IsConnected;
            }
        }

        public Cognito2Desk(string host) {
            deskConnection = new TelnetConnection(host, 11123);
            if (!deskConnection.IsConnected) throw new InvalidOperationException("Unable to make initial connection to desk at [" + host + "]");
        }

        public bool AttributeFade(uint fixture, uint percent) {
            return AttributeFade(fixture, percent, 0);
        }

        public bool AttributeFade(uint fixture, uint percent, double fadeTime) {
            return AttributeFade(fixture, INTENSITY, percent, fadeTime);
        }

        public bool AttributeFade(uint fixture, string channel, uint percent) {
            return AttributeFade(fixture, channel, percent, 0);
        }

        public bool AttributeFade(uint fixture, string channel, uint percent, double fadeTime) {
            if (IsConnected) {
                deskConnection.WriteLine("API.AttributeFade(" + fixture + ",'" + channel + "'," + percent + "," + fadeTime .ToString("0.00") + ")");
                string result = deskConnection.Read();
                if (result.Equals(PROMPT)) return true;
                else {
                    // TODO: Log Error
                }
            } else {
                // TODO: Log connection error
            }
            return false;
        }

        public bool AttributeFadeCapture(uint fixture, uint percent) {
            return AttributeFadeCapture(fixture, percent, 0);
        }

        public bool AttributeFadeCapture(uint fixture, uint percent, double fadeTime) {
            return AttributeFadeCapture(fixture, INTENSITY, percent, fadeTime);
        }

        public bool AttributeFadeCapture(uint fixture, string channel, uint percent) {
            return AttributeFadeCapture(fixture, channel, percent, 0);
        }

        public bool AttributeFadeCapture(uint fixture, string channel, uint percent, double fadeTime) {
            if (IsConnected) {
                deskConnection.WriteLine("API.AttributeFadeCapture(" + fixture + ",'" + channel + "'," + percent + "," + fadeTime.ToString("0.00") + ")");
                string result = deskConnection.Read();
                if (result.Equals(PROMPT)) return true;
            } else {
                // TODO: Log connection error
            }
            return false;
        }
    }
}