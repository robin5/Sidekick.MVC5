using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PEClient.Models
{
    public class ToneAnalysisViewModel
    {
        public ToneAnalysisViewModel(int toneIndex)
        {
            ToneIndex = toneIndex;
        }
        public int ToneIndex { get; private set; }
    }
}