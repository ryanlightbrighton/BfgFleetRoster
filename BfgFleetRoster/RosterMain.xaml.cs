using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BfgFleetRoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RosterMain : Window
    {
        // classes to represent the various navies

        private class BfgFaction
        {
            public string DisplayName { get; private set; }
            public string FactionName { get; private set; }

            public string[] SpecialRules { get; private set; } //  think about this later for example LORD ADMIRAL ZACCARIUS RATH on Bakka Fleets

            public BfgFaction(string d, string f)
            {
                DisplayName = d;
                FactionName = f;
            }
        }

        private enum FireArcEnum
        {
            L,
            R,
            F,
            LFR
        }

        // class for weapons

        private class Weapon
        {
            public int WeapID { get; private set; }
            public string DisplayName { get; private set; }
            public string RangeSpeed { get; private set; }
            public int FirePowerStrength { get; private set; }
            public FireArcEnum FireArc { get; private set; }

            public Weapon(int id, string d, string rs, int fps, FireArcEnum fa)
            {
                WeapID = id;
                DisplayName = d;
                RangeSpeed = rs;
                FirePowerStrength = fps;
                FireArc = fa;
            }

            public Weapon(int id, string d, string rs, int fps, int fa)
            {
                WeapID = id;
                DisplayName = d;
                RangeSpeed = rs;
                FirePowerStrength = fps;
                FireArc = (FireArcEnum)fa;
            }
        }

        // class for ships

        private class BfgShip
        {
            public int ShipID { get; private set; }
            public string PlayerShipName { get; private set; }
            public string DisplayName { get; private set; }
            public int PointsCostBase { get; private set; }

            public int Type { get; private set; }
            // need property for file picture

            // in game properties

            // base properties

            public int HitsBase { get; private set; }
            public int SpeedBase { get; private set; }
            public int TurnsBase  { get; private set; }
            public int ShieldsBase  { get; private set; }

            public int ArmorProwBase { get; private set; }
            public int ArmorSideBase { get; private set; }
            public int ArmorRearBase { get; private set; }
            public int TurretsBase { get; private set; }
            public int WeaponProwBase { get; private set; }
            public int WeaponLeft1Base { get; private set; }
            public int WeaponLeft2Base { get; private set; }
            public int WeaponLeft3Base { get; private set; }
            public int WeaponRight1Base { get; private set; }
            public int WeaponRight2Base { get; private set; }
            public int WeaponRight3Base { get; private set; }
            public int WeaponDorsalBase { get; private set; }
            public string SpecialRulesBase { get; private set; }

            public List<Weapon> WeaponsBase { get; set; }
            // variable properties

            public bool Crippled { get; private set; }
            public bool Braced { get; private set; }
            public int Hits { get; private set; }
            public int Speed { get; private set; }
            public int Turns { get; private set; }
            public int WeaponProw { get; private set; }
            public int WeaponLeft1 { get; private set; }
            public int WeaponLeft2 { get; private set; }
            public int WeaponLeft3 { get; private set; }
            public int WeaponRight1 { get; private set; }
            public int WeaponRight2 { get; private set; }
            public int WeaponRight3 { get; private set; }
            public int WeaponDorsal { get; private set; }

            // this constructor creates the ship in the types menu

            public BfgShip(int id, string n, int p, int typ, int hits, int spd, int trns, int shld, int armP, int armS, int armR, int turr, string sr)
            {
                ShipID = id;
                DisplayName = n;
                PointsCostBase = p;
                Type = typ;
                HitsBase = hits;
                SpeedBase = spd;
                TurnsBase = trns;
                ShieldsBase = shld;
                ArmorProwBase = armP;
                ArmorSideBase = armS;
                ArmorRearBase = armR;
                TurretsBase = turr;
                SpecialRulesBase = sr;
            }

            

            // this constructor
        }

        // class for squadrons





        public RosterMain()
        {
            InitializeComponent();

            // create factions

            List<BfgFaction> factionList = new List<BfgFaction>();

            factionList.Add(new BfgFaction("Segmentum Obscurus Gothic Sector","imp_sogs"));
            factionList.Add(new BfgFaction("Segmentum Obscurus Bastion", "imp_sob"));
            factionList.Add(new BfgFaction("Segmentum Solar Armageddon", "imp_ssa"));
            factionList.Add(new BfgFaction("Segmentum Tempestus Bakka", "imp_stb"));
            factionList.Add(new BfgFaction("Inquisitorial Fleet Detachment", "inq_fd"));
            factionList.Add(new BfgFaction("Ordo Malleus Banisher Fleet Detachment", "inq_omb"));

            // init the CB_NavySelector values here

            //DG_ShipTypes.ItemsSource = SqliteDataAccess.LoadRoster().DefaultView; // load DataGrid with DataTable

            DataTable d = SqliteDataAccess.LoadRoster();


            // add list of ships

            List<BfgShip> ships = new List<BfgShip>();

            

            foreach (DataRow row in d.AsEnumerable())
            {
                int shipID = Convert.ToInt32(row.Field<long>("ship_ID"));
                string name = row.Field<string>("ship_name");
                int points = Convert.ToInt32(row.Field<long>("ship_points"));
                int type = Convert.ToInt32(row.Field<long>("ship_type"));
                int hits = Convert.ToInt32(row.Field<long>("ship_hits"));
                int speed = Convert.ToInt32(row.Field<long>("ship_speed"));
                int turns = Convert.ToInt32(row.Field<long>("ship_turns"));
                int shields = Convert.ToInt32(row.Field<long>("ship_shields"));
                int armorProw = Convert.ToInt32(row.Field<long>("ship_armor_prow"));
                int armorSide = Convert.ToInt32(row.Field<long>("ship_armor_side"));
                int armorRear = Convert.ToInt32(row.Field<long>("ship_armor_rear"));
                int turrets = Convert.ToInt32(row.Field<long>("ship_turrets"));
                string specialRules = row.Field<string>("ship_special_rules");

                BfgShip b = new BfgShip(shipID, name, points, type, hits, speed, turns, shields, armorProw, armorSide, armorRear, turrets, specialRules);



                // now add the weapons for this shipclass

                DataTable dt = SqliteDataAccess.LoadWeapons(shipID);

                List<Weapon> weaps = new List<Weapon>();

                foreach (DataRow rowWeap in dt.AsEnumerable())
                {
                    int wID = Convert.ToInt32(rowWeap.Field<long>("weap_ID"));
                    string wName = rowWeap.Field<string>("weap_display_name");
                    string wRangeSpeed = rowWeap.Field<string>("weap_range_speed");
                    int wFirepowerStrength = Convert.ToInt32(rowWeap.Field<long>("weap_firepower_strength"));
                    int wFireArc = Convert.ToInt32(rowWeap.Field<long>("weap_fire_arc"));

                    Weapon w = new Weapon(wID, wName, wRangeSpeed, wFirepowerStrength, wFireArc);

                    weaps.Add(w);
                }

                b.WeaponsBase = weaps;

                ships.Add(b);
            }

            LB_ShipTypes.ItemsSource = ships;
            LB_ShipTypes.DataContext = ships;
            LB_ShipTypes.DisplayMemberPath = "DisplayName";

            //LB_ShipTypes.ToolTip = $"xxxx{Environment.NewLine}yyyyyy{Environment.NewLine}zzzzzzzzz";
        }

        private List<BfgShip>RosterShips = new List<BfgShip>();

        

        private void BTN_AddShip_Click(object sender, RoutedEventArgs e)
        {
            BfgShip selectedShip = (BfgShip)LB_ShipTypes.SelectedItem;
            int.TryParse(TB_Points.Text, out int num);
            num += selectedShip.PointsCostBase;
            TB_Points.Text = num.ToString();
            RosterShips.Add(selectedShip);
            LB_Roster.ItemsSource = RosterShips;
            LB_Roster.DisplayMemberPath = "DisplayName";
            LB_Roster.Items.Refresh();
        }

        private void BTN_RemoveShips_Click(object sender, RoutedEventArgs e)
        {
            //List<BfgShip> removeShips = new List<BfgShip>();
            int.TryParse(TB_Points.Text, out int num);
            for (int i = 0; i < LB_Roster.SelectedItems.Count; i++)
            {
                BfgShip s = (BfgShip)LB_Roster.SelectedItems[i];
                RosterShips.Remove(s);
                
                num -= s.PointsCostBase;
            }
            TB_Points.Text = num.ToString();
            LB_Roster.Items.Refresh();

        }
    }
}
