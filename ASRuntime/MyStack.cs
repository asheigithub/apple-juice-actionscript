using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ASRuntime
{
	class MyStack : IEnumerable<StackFrame>
	{
		private StackFrame[] inner;

		int pos;

		public MyStack(int maxcount)
		{
			inner = new StackFrame[maxcount];
			pos = 0;
		}

		public int Count
		{
			get
			{
				return pos;
			}
		}

		public void Push(StackFrame stackFrame)
		{
			inner[pos++] = stackFrame;
		}

		public StackFrame Peek()
		{
			return inner[pos-1];
		}

		public StackFrame Pop()
		{
			var f = inner[pos-1];

			inner[--pos] = null;

			return f;

		}


		public void Clear()
		{
			for (int i = 0; i < pos; i++)
			{
				inner[i] = null;
			}
			pos = 0;
		}

		public IEnumerator<StackFrame> GetEnumerator()
		{
			for (int i = pos-1; i >0; i--)
			{
				yield return inner[i];
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
