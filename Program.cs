// Write a C# function float RMSD( atom[] referenceFrame,
// atom[] frame) to calculate the RMSD between two frames. [Implement required classes].

//These are things provided you can't change, as you need them, libraries and stuff
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioPhysics_CellsCreation
{
    internal class Program
    {
        //First required class to be created, Atom class
        class atom
        {
            private float xpos, ypos, zpos, vel, acc;
            private atom Next = null;

            public atom() { }
            public atom(float x = 0f, float y = 0f, float z = 0f, float v = 0f, float a = 0f)
            {
                this.xpos = x;
                this.ypos = y;
                this.zpos = z;
                this.vel = v;
                this.acc = a;
            }
            public void setX(float x = 0f)
            {
                this.xpos = x;
            }
            public void setY(float y = 0f)
            {
                this.ypos = y;
            }
            public void setZ(float z = 0f)
            {
                this.zpos = z;
            }
            public void setVel(float v = 0f)
            {
                this.vel = v;
            }
            public void setAcc(float a = 0f)
            {
                this.acc = a;
            }
            public void setNext(atom next)
            {
                this.Next = next;
            }
            public void setAtom(atom Atom)
            {
                this.xpos = Atom.xpos;
                this.ypos = Atom.ypos;
                this.zpos = Atom.zpos;
                this.vel = Atom.vel;
                this.acc = Atom.acc;
                this.Next = Atom.Next;
            }
            public float getX()
            {
                return this.xpos;
            }
            public float getY()
            {
                return this.ypos;
            }
            public float getZ()
            {
                return this.zpos;
            }
            public float getVel()
            {
                return this.vel;
            }
            public float getAcc()
            {
                return this.acc;
            }
            public atom getNext()
            {
                return this.Next;
            }
            public atom getAtom()
            {
                return this;
            }
        }

        //Second class to be created, class Program
        class program
        {
            public void displayCell(atom head)
            {
                atom curr = head;
                int count = 1;
                while (curr != null)
                {
                    Console.WriteLine("\n\nAtom Info number:    " + count);
                    Console.WriteLine("X-Axis Position :    " + curr.getX());
                    Console.WriteLine("Y-Axis Position :    " + curr.getY());
                    Console.WriteLine("Z-Axis Position :    " + curr.getZ());
                    Console.WriteLine("Velocity Value :     " + curr.getVel());
                    Console.WriteLine("Acceleration Value : " + curr.getAcc());
                    count++;
                    curr = curr.getNext();
                }
            }
            public atom readCellData(int noAtoms)
            {
                float x, y, z, v, a = 0f;
                atom head = new atom(), curr = new atom(), prev = new atom();
                for (int i = 0; i < noAtoms; i++)
                {
                    Console.WriteLine("Please enter the Info of atom number " + (i + 1) + ":::::");
                    Console.WriteLine("Please enter the X-Position value   : ");
                    x = float.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the Y-Position value   : ");
                    y = float.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the Z-Position value   : ");
                    z = float.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the Velocity value     : ");
                    v = float.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the Acceleration value : ");
                    a = float.Parse(Console.ReadLine());

                    atom atom = new atom(x, y, z, v, a);

                    if (i == 0)
                    {
                        head = atom;
                        prev = head;
                    }
                    else
                    {
                        curr = atom;
                        prev.setNext(curr);
                        prev = curr;
                    }
                }
                return head;
            }
            public float RMSD(atom referenceFrame,atom frame)
            {
                float rmsd = 0f , dx =0f, dy =0f, dz =0f, dist =0f;

                atom headRef = referenceFrame;
                atom headFrame = frame;
                int Count = 0;

                while (true)
                {
                    if( headFrame != null && headRef != null)
                    {
                        dx = headRef.getX() - headFrame.getX();
                        dy = headRef.getY() - headFrame.getY();
                        dz = headRef.getZ() - headFrame.getZ();
                        dist += (dx * dx + dy * dy + dz * dz);
                        Count = Count + 1;
                        headFrame = headFrame.getNext();
                        headRef = headRef.getNext();
                    }
                    else if (headFrame != null || headRef != null)
                    {
                        Console.WriteLine("ERROR, RMSD Calculation requires 2 molecules to have same number of atoms.");
                        return -1;
                    }
                    else if (headFrame == null && headRef == null)
                    {
                        rmsd = MathF.Sqrt(dist / (Count*2));
                        return rmsd;
                    }
                }
            }

            public atom[] main(int noCells)
            {
                atom head = new atom();
                atom[] arrCells = new atom[noCells];
                for (int i = 0; i < noCells; i++)
                {
                    Console.WriteLine("\n\nEnter how many atoms are in cell number " + (i + 1));
                    int noAtoms = int.Parse(Console.ReadLine());
                    head = readCellData(noAtoms);
                    displayCell(head);
                    arrCells[i] = head;
                }
                return arrCells;
            }
        }

        //Our main function where C# starts being executed for the console application to be run
        static void Main(string[] args)
        {
            int noCells = 3;

            program MyCells = new program();
            atom[] cells = MyCells.main(noCells);

           for(int i = 0; i < cells.Length-1; i++)
            {
                for (int j = i+1; j < cells.Length; j++)
                {
                    float rmsd = MyCells.RMSD(cells[i], cells[j]);
                    Console.WriteLine("\n\nThe RMSD of cell "+(i+1).ToString()+" and cell " +(j+1).ToString() +" is : " + rmsd.ToString()+"\n\n");

                }
            }
        }
    }
}