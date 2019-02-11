﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoRestService.Common
{
    public interface LocationProvider
    {
        double getLatitude();

        double getLongitude();
    }
}
