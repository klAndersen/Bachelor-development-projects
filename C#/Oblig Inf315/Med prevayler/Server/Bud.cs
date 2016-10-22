using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server {
    [Serializable]
    public class Bud {
        private int belop;

        public Bud(int belop) {
            this.belop = belop;
        } //Konstruktør

        public int getBelop() {
            return belop;
        } //getBelop

        public override string ToString() {
            return "Bud: Kr. " + belop.ToString();
        } //ToString
    } //Bud
} //namespace