using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTStest
{
	public class PowerTTS
	{

		//public string Message { get; set; }
		private const string DLLFileName = @"PowerTTS_M.dll";

		public const string OEMKeyNumber = "u5j6ANPZtNdBs5XZplfZWQAAu$fZu5fZu5fZuWBeOW4THbpku5d8ZbdfEpw@3NfZ2T$bG5f@!!zp@PPq$7Tv$mfgy#jlzO!mw6Dbw8^^";

		[DllImport(DLLFileName, EntryPoint = "PTTS_Initialize")]
		public static extern int PTTS_Initialize();

		[DllImport(DLLFileName)]
		public static extern void PTTS_UnInitialize();

		[DllImport(DLLFileName)]
		public static extern void PTTS_SetOemKey(string OemKey);

		[DllImport(DLLFileName)]
		public static extern int PTTS_LoadEngine(int Language, string DbDir, int bLoadInMemory);

		[DllImport(DLLFileName)]
		public static extern void PTTS_UnLoadEngine(int Language);

		[DllImport(DLLFileName)]
		public static extern int PTTS_PlayTTS(IntPtr hUsrWnd, uint uUsrMsg, string TextBuf, string tagString, int Language, int SpeakerID);

		[DllImport(DLLFileName)]
		public static extern int PTTS_StopTTS();

		[DllImport(DLLFileName)]
		public static extern int PTTS_PauseTTS();

		[DllImport(DLLFileName)]
		public static extern int PTTS_RestartTTS();

		[DllImport(DLLFileName)]
		public static extern int PTTS_TextToFile(int Language, int SpeakerID, int Format, int Encoding, int SamplingRate,
					  string TextBuf, string OutFileName, string tagString, string UserDictFileName);

		private const int PTTS_PLAY_START = 1;
		private const int TTS_PLAY_PAUSE = 2;
		private const int PTTS_PLAY_RESTART = 3;
		private const int PTTS_PLAY_END = 4;
		private const int PTTS_PLAY_ERROR = 5;
		//System.Threading.DispatcherTimer _dt재생 = null;
		public void Open()
		{
			try
			{
				//   ThreadExitEvent = new AutoResetEvent(false);                
				int nInit = PTTS_Initialize();
				//TraceManager.AddInfoLog(string.Format("PTTS_Initialize : {0}", nInit));
				PTTS_SetOemKey(OEMKeyNumber);
				int nEngine = PTTS_LoadEngine(0, "PowerTTS_M_DB\\KO_KR\\", 0);
				Console.WriteLine($"ENDGINE: {nEngine}");
				//TraceManager.AddInfoLog(string.Format("PTTS_LoadEngine : {0}", nEngine));

				//if (_dt재생 == null)
				//{
				//	_dt재생 = new System.Windows.Threading.DispatcherTimer();
				//	_dt재생.Tick += _dt재생_Tick;
				//}
				//_dt재생.Tag = 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine(string.Format("### {0}\r\n{1} ###", ex.StackTrace, ex.Message));
			}
		}

		void Close()
		{
			try
			{
				PTTS_UnLoadEngine(0);
				PTTS_UnInitialize();
			}
			catch (Exception ex)
			{
				//TraceManager.AddLog(string.Format("### {0}\r\n{1} ###", ex.StackTrace, ex.Message));
			}
		}

		public void DoFinal()
		{
			try
			{
				Close();

				//if (ThreadExitEvent != null)
				//{
				//    ThreadExitEvent.Reset();
				//    ThreadExitEvent.Dispose();
				//    ThreadExitEvent = null;
				//}

				GC.Collect();
			}
			catch (Exception ex)
			{
				//TraceManager.AddLog(string.Format("### {0}\r\n{1} ###", ex.StackTrace, ex.Message));
			}
		}

		

		bool IsSoundPlay = false;
		public void Play(string Message)
		{
			try
			{
				if (IsSoundPlay == true)
				{
					//TraceManager.AddTTSLog(string.Format("현재 TTS재생중이라 생략 : {0}", Message));
					//TraceManager.AddInfoLog(string.Format("현재 TTS재생중이라 생략 : {0}", Message));
				}
				else
				{
					IsSoundPlay = true;
					IntPtr intptr = new IntPtr();
					PTTS_PlayTTS(intptr, 0, Message, "", 0, 0);

					//if (_dt재생 == null)
					//{
					//	_dt재생 = new System.Windows.Threading.DispatcherTimer();
					//	_dt재생.Tick += _dt재생_Tick;
					//}
					//_dt재생.Tag = 0;
					//_dt재생.Interval = TimeSpan.FromMilliseconds((0.18 * Message.Length) * 1000);
					//_dt재생.Start();
				}
			}
			catch (AccessViolationException ee)
			{
				Console.WriteLine(string.Format("### {0}\r\n{1} ###", ee.StackTrace, ee.Message));
				Close();
				Open();
			}
		}

		//private void _dt재생_Tick(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		if (Convert.ToInt32(_dt재생.Tag) == 1) return;
		//		_dt재생.Tag = 1;
		//		_dt재생.Stop();

		//		IsSoundPlay = false;

		//		_dt재생.Tag = 0;
		//	}
		//	catch (Exception ee)
		//	{
		//		TraceManager.AddLog(string.Format("### {0}\r\n{1} ###", ee.StackTrace, ee.Message));
		//		System.Diagnostics.Debug.WriteLine(string.Format("### {0}\r\n{1} ###", ee.StackTrace, ee.Message));
		//		_dt재생.Tag = 0;
		//	}
		//}

	}
}
