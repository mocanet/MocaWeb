
<Assembly: WebActivator.PreApplicationStartMethod(GetType(Moca.Di.MocaContainerFactory), "Init")> 
<Assembly: WebActivator.PreApplicationStartMethod(GetType(Moca.Configuration.SectionProtector), "Protect")> 
<Assembly: WebActivator.ApplicationShutdownMethod(GetType(Moca.Di.MocaContainerFactory), "Destroy")> 
