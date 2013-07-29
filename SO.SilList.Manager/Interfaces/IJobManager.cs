﻿using SO.SilList.Manager.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.SilList.Manager.Interfaces
{
    interface IJobManager
    {
        JobVo get(Guid jobId);
        List<JobVo> getAll(bool? isActive = true);
        bool delete(Guid jobId);
        JobVo update(JobVo input, Guid? jobId = null);
        JobVo insert(JobVo input);
    }
}
