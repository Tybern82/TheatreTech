using System;
using System.Collections.Generic;
using System.Text;
using MinimalisticTelnet;

namespace Cognito2Desk {
    public enum BumpDirection { UP, DOWN };
    public enum LogSeverity { SEVERITY_1, SEVERITY_2, SEVERITY_3, SEVERITY_4, SEVERITY_5, SEVERITY_6, SEVERITY_7, SEVERITY_8, SEVERITY_9, SEVERITY_10}

    public class Cognito2Desk {
        string PROMPT = "API>";

        public string INTENSITY = "Intensity";
        public string RED_CYC = "Color A";
        public string GREEN_CYC = "Color B";
        public string BLUE_CYC = "Color C";

        private TelnetConnection deskConnection;

        public bool IsConnected {
            get {
                return (deskConnection != null) ? deskConnection.IsConnected : false;
            }
        }

        public Cognito2Desk(string host) {
            deskConnection = new TelnetConnection(host, 11123);
            if (!deskConnection.IsConnected) throw new InvalidOperationException("Unable to make initial connection to desk at [" + host + "]");
        }

        private bool executeCommand(string command) {
            if (IsConnected) {
                deskConnection.WriteLine(command);
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

        #region AttributeFade API.AttributeFade(fixture[,attribute_name],value [,time])
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
            return executeCommand("API.AttributeFade(" + fixture + ",'" + channel + "'," + percent + "," + fadeTime .ToString("0.00") + ")");
        }
        #endregion

        #region AttributeFadeCapture API.AttributeFadeCapture(fixture[,attribute_name],value [,time])
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
            return executeCommand("API.AttributeFadeCapture(" + fixture + ",'" + channel + "'," + percent + "," + fadeTime.ToString("0.00") + ")");
        }
        #endregion

        #region Bump API.Bump('page_name' | page_index , memory_number, is_down )
        public bool BumpUp(uint pageIndex, uint memoryNumber) {
            return Bump(pageIndex, memoryNumber, BumpDirection.UP);
        }

        public bool BumpDown(uint pageIndex, uint memoryNumber) {
            return Bump(pageIndex, memoryNumber, BumpDirection.DOWN);
        }

        public bool Bump(uint pageIndex, uint memoryNumber, BumpDirection direction) {
            return executeCommand("API.Bump(" + pageIndex + ", " + memoryNumber + ", " + (direction == BumpDirection.DOWN) + ")");
        }

        public bool BumpUp(string pageName, uint memoryNumber) {
            return Bump(pageName, memoryNumber, BumpDirection.UP);
        }

        public bool BumpDown(string pageName, uint memoryNumber) {
            return Bump(pageName, memoryNumber, BumpDirection.DOWN);
        }

        public bool Bump(string pageName, uint memoryNumber, BumpDirection direction) {
            return executeCommand("API.Bump('" + pageName + "', " + memoryNumber + ", " + (direction == BumpDirection.DOWN) + ")");
        }
        #endregion

        #region ButtonPress API.ButtonPress('page','name' or order)
        public bool ButtonPress(string pageName, string buttonName) {
            return executeCommand("API.ButtonPress('" + pageName + "', '" + buttonName + "')");
        }

        // TODO-TEST: public bool ButtonPress(string pageName, ????? buttonOrder)
        #endregion

        #region Memory
        #region MemoryFade API.MemoryFade('page',memorynumber,value[, seconds])

        #endregion

        #region MemoryFadeRate API.MemoryFadeRate('page',memorynumber,value[, seconds full scale])

        #endregion

        #region MemoryFadeStop API.MemoryFadeStop('page',memorynumber)

        #endregion

        #region MemoryGetValue print(API.MemoryGetValue('page', memorynumber))

        #endregion
        #endregion

        #region MIDI
        #region MidiNoteOff API.MidiNoteOff(channel_1_to_16, key_1_to_128[, velocity_0_to127])

        #endregion

        #region MidiNoteOn API.MidiNoteOn(channel_1_to_16, key_1_to_128[, velocity_0_to127])

        #endregion

        #region MidiWrite API.MidiWrite(midi_byte[, midi_byte...])

        #endregion
        #endregion

        #region PlayList
        #region PlayListAssert API.PlayListAssert('playlist')

        #endregion

        #region PlayListGo API.PlayListGo('playlist')

        #endregion

        #region PlayListGotoAndExecuteFollows API.PlayListGotoAndExecuteFollows('playlist', cue)

        #endregion

        #region PlayListGotoAndHalt API.PlayListGotoAndHalt('playlist', cue)

        #endregion

        #region PlayListHalt API.PlayListHalt('playlist')

        #endregion

        #region PlayListHaltBack API.PlayListHaltBack('playlist')

        #endregion

        #region PlayListRelease API.PlayListRelease('playlist'[, release_time])

        #endregion
        #endregion

        #region System
        #region ReleaseAll API.ReleaseAll()
        public bool ReleaseAll() {
            return executeCommand("API.ReleaseAll()");
        }
        #endregion

        #region SetLevel API.SetLevel('fixture_string', 'level_string' [, fade_time_seconds])

        #endregion

        #region SystemRestart API.SystemRestart([< maintain state > true | false])
        public bool SystemRestart() {
            return executeCommand("API.SystemRestart()");
        }

        public bool SystemRestart(bool maintainState) {
            return executeCommand("API.SystemRestart(" + maintainState + ")");
        }
        #endregion

        #region SystemShutdown API.SystemShutdown()
        public bool SystemShutdown() {
            return executeCommand("API.SystemShutdown()");
        }
        #endregion

        #region WriteLogMessage API.WriteLogMessage('message', 'category', severity_1_to_10)
        // TODO: Category to enum of valid category items
        public bool WriteLogMessage(string message, string category, LogSeverity severity) {
            return executeCommand("API.WriteLogMessage('" + message + "', '" + category + "', " + getLogSeverity(severity) + ")");
        }

        uint getLogSeverity(LogSeverity severity) {
            switch (severity) {
                case LogSeverity.SEVERITY_1: return 1;
                case LogSeverity.SEVERITY_2: return 2;
                case LogSeverity.SEVERITY_3: return 3;
                case LogSeverity.SEVERITY_4: return 4;
                case LogSeverity.SEVERITY_5: return 5;
                case LogSeverity.SEVERITY_6: return 6;
                case LogSeverity.SEVERITY_7: return 7;
                case LogSeverity.SEVERITY_8: return 8;
                case LogSeverity.SEVERITY_9: return 9;
                case LogSeverity.SEVERITY_10:
                default:
                    return 10;
            }
        }
        #endregion
        #endregion
    }
}