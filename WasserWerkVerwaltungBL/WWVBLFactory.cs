using System;
using System.Collections.Generic;
using System.Text;

namespace WasserWerkVerwaltung.BL {
    public class WWVBLFactory {
        public static IWWVBL GetBussinessLogicObject() {
            return new WWVBusinessObject();
        }
    }
}
