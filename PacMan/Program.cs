using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace PacMan
{
    internal class Program
    {
        // PACMAN
        const float PI = 3.14f;
        static float tx = 0.1f;
        static float ty = 0.1f;
        const float step = 0.01f;
        static float xstep = 0.01f;
        static float ystep = 0.01f;
        static bool pacman = false;
        static void PacMan()
        {
            Gl.glColor3f(1.0f, 1.0f, 0.0f);
            Gl.glLineWidth(15);
            float raio = 0.1f;
            Gl.glPushMatrix();
            Gl.glTranslatef(tx, ty, 0.0f);
            if (pacman)
            {
                desenhaPacMan(raio);
                Gl.glColor3f(0.0f, 0.0f, 0.0f);
                Gl.glLineWidth(5);
                desenhaContornoPacMan(raio);
                Gl.glColor3f(0.0f, 0.0f, 0.0f);
            }

            if (!pacman)
            {
                desenhaPacManFechado(raio);
                Gl.glColor3f(0.0f, 0.0f, 0.0f);
                Gl.glLineWidth(5);
                desenhaContornoPacManFechado(raio);
                Gl.glColor3f(0.0f, 0.0f, 0.0f);
            }

            desenhaOlho(0.01f);
            Gl.glPopMatrix();
        }
        static void desenhaPacMan(float raio)
        {
            float x, y, pontos;
            pontos = (2 * PI) / 50;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex2f(0.0f, 0.0f);
            for (float angulo = 5 * pontos; angulo <= 2 * PI - (5 * pontos); angulo +=
            pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }
        static void Timer(int value)
        {
            if (tx > 0.90 || tx < 0.0) 
                xstep = -xstep;
            if (ty > 0.90 || ty < 0.0) 
                ystep = -ystep;
            tx += xstep;
            ty += ystep;
            tx += xstep;
            ty += ystep;
            Glut.glutPostRedisplay();
            pacman = !pacman;
            Glut.glutTimerFunc(100, Timer, 1);
        }
        static void desenhaPacManFechado(float raio)
        {
            float x, y, pontos;
            pontos = (2 * PI) / 150;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex2f(0.0f, 0.0f);
            for (float angulo = 5 * pontos; angulo <= 2 * PI - (5 * pontos); angulo +=
            pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }
        static void desenhaContornoPacMan(float raio)
        {
            float x, y, pontos;
            pontos = (2 * PI) / 50;
            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2f(0.0f, 0.0f);
            for (float angulo = 5 * pontos; angulo <= 2 * PI - (5 * pontos); angulo +=
            pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }

        static void desenhaContornoPacManFechado(float raio)
        {
            float x, y, pontos;
            pontos = (2 * PI) / 150;
            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glVertex2f(0.0f, 0.0f);
            for (float angulo = 5 * pontos; angulo <= 2 * PI - (5 * pontos); angulo +=
            pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }
        static void desenhaOlho(float raio)
        {
            float x, y, pontos;
            pontos = (2 * PI) / 500;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            for (float angulo = 0.0f; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo));
                y = (float)(raio * Math.Sin(angulo));
                Gl.glVertex2f(x, y + 0.05f);
            }
            Gl.glEnd();
        }
        static void TeclasEspeciais(int key, int x, int y)
        {
            
            const float raio = 0.1f;
            switch (key)
            {
                case Glut.GLUT_KEY_UP:
                    ty += step;
                    break;
                case Glut.GLUT_KEY_DOWN:
                    ty -= step;
                    break;
                case Glut.GLUT_KEY_LEFT:
                    tx -= step;
                    break;
                case Glut.GLUT_KEY_RIGHT:
                    tx += step;
                    break;
            }
            pacman = !pacman;
            if (tx < raio)
            {
                tx = raio;
            }
            else if (tx > 1.0f - raio)
            {
                tx = 1.0f - raio;
            }

            if (ty < raio)
            {
                ty = raio;
            }
            else if (ty > 1.0f - raio)
            {
                ty = 1.0f - raio;
            }
            Glut.glutPostRedisplay();
        }
        static void display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            PacMan();
            Glut.glutSwapBuffers(); //Atualiza o BUFFER
        }
        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0f, 1.0f, 0.0f, 1.0f);
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
        }
        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(600, 600);
            Glut.glutCreateWindow("Projeto");
            inicializa();
            Glut.glutDisplayFunc(display);
            //Glut.glutSpecialFunc(TeclasEspeciais);
            Glut.glutTimerFunc(100, Timer, 1);
            Glut.glutMainLoop();
        }
    }
}
