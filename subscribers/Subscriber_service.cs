//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace subscribers
{
    using System;
    using System.Collections.Generic;
    
    public partial class Subscriber_service
    {
        public int id_subscriber_service { get; set; }
        public int id_service { get; set; }
        public int id_subcriber { get; set; }
    
        public virtual Services Services { get; set; }
        public virtual Subscriber Subscriber { get; set; }
    }
}
