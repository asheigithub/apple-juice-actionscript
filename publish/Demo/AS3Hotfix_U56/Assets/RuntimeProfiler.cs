using UnityEngine;
using UnityEngine.Profiling;

public class RuntimeProfiler : MonoBehaviour
{
	public static bool isShowInfo = true;

	private int frames = 0;

	const float fpsMeasurePeriod = 0.5f;
	private int m_FpsAccumulator = 0;
	private float m_FpsNextPeriod = 0;
	private int m_CurrentFps;


	void Start()
	{
		rect = new Rect(2, 2, 120, 80);
	}

	void Update()
	{
		++frames;
		m_FpsAccumulator++;
		if (Time.realtimeSinceStartup > m_FpsNextPeriod)
		{
			m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
			m_FpsAccumulator = 0;
			m_FpsNextPeriod += fpsMeasurePeriod;
		}
	}

	private Rect rect;
	string info = "";

	void OnGUI()
	{
		if (!isShowInfo)
		{
			return;
		}

		if (Time.frameCount % 10 == 0)
		{
			info = "" + m_CurrentFps + "帧          \n"
				+ (Profiler.GetTotalAllocatedMemoryLong() / 1024.0f / 1024.0f).ToString("0.0") + " M 申请\r\n"
				+ (Profiler.GetTotalReservedMemoryLong() / 1024.0f / 1024.0f).ToString("0.0") + " M 保留\r\n"
				+ (Profiler.GetTotalUnusedReservedMemoryLong() / 1024.0f / 1024.0f).ToString("0.0") + " M 未使用\r\n";
		}

		GUI.Box(rect, info);


	}
}

