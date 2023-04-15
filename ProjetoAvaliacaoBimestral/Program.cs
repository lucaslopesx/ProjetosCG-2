using System;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;

namespace Projeto
{
    class Program
    {
        static float rot = 0.0f;
        static float carPositionX = 0.0f;
        const float PI = 3.14f;

        static void desenhaCirculo(bool isFrontWheel)
        {
            float positionX = carPositionX;
            if (isFrontWheel)
            {
                positionX += 0.4f;
            }
            else
            {
                positionX += -0.4f;
            }

            Gl.glPushMatrix();
            Gl.glTranslatef(positionX, 0.0f, 0.0f);
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);

            // Draw the tire
            float raio = 0.1f;
            int numSegments = 30;
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < numSegments; i++)
            {
                float angle = (float)(2 * PI * i / numSegments);
                float x = (float)(raio * Math.Cos(angle));
                float y = (float)(raio * Math.Sin(angle));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            // Draw the rim
            raio = 0.05f;
            Gl.glColor3f(0.7f, 0.7f, 0.7f);
            Gl.glBegin(Gl.GL_POLYGON);
            for (int i = 0; i < numSegments; i++)
            {
                float angle = (float)(2 * PI * i / numSegments);
                float x = (float)(raio * Math.Cos(angle));
                float y = (float)(raio * Math.Sin(angle));
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            // Draw lines on the rim
            int numLines = 8;
            Gl.glColor3f(0.3f, 0.3f, 0.3f);
            Gl.glBegin(Gl.GL_LINES);
            for (int i = 0; i < numLines; i++)
            {
                float angle = (float)(2 * PI * i / numLines);
                float x1 = (float)(raio * Math.Cos(angle));
                float y1 = (float)(raio * Math.Sin(angle));
                float x2 = (float)((raio * 1.75) * Math.Cos(angle));
                float y2 = (float)((raio * 1.75) * Math.Sin(angle));
                Gl.glVertex2f(x1, y1);
                Gl.glVertex2f(x2, y2);
            }
            Gl.glEnd();

            Gl.glPopMatrix();
        }

        static void desenhaCarro()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(carPositionX, 0.0f, 0.0f); // Use the carPositionX variable for translation

            // Draw the car body
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glVertex2f(-0.6f, 0.1f);
            Gl.glVertex2f(0.6f, 0.1f);
            Gl.glVertex2f(0.6f, 0.2f);
            Gl.glVertex2f(0.4f, 0.35f);
            Gl.glVertex2f(-0.4f, 0.35f);
            Gl.glVertex2f(-0.6f, 0.2f);
            Gl.glEnd();

            // Draw the car roof
            Gl.glColor3f(0.7f, 0.0f, 0.0f);
            Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glVertex2f(-0.4f, 0.35f);
            Gl.glVertex2f(0.4f, 0.35f);
            Gl.glVertex2f(0.3f, 0.5f);
            Gl.glVertex2f(-0.3f, 0.5f);
            Gl.glEnd();

            // Draw the windows
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glBegin(Gl.GL_QUADS);


            // Right window
            Gl.glVertex2f(0.0f, 0.4f);
            Gl.glVertex2f(0.3f, 0.4f);
            Gl.glVertex2f(0.3f, 0.47f);
            Gl.glVertex2f(0.0f, 0.47f);

            Gl.glEnd();
            Gl.glPopMatrix();
        }





        static void display()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            rot -= 0.8f; // Add this line to increment the rotation value

            desenhaCirculo(true);
            desenhaCirculo(false);
            desenhaCarro();

            Glut.glutSwapBuffers();
            Glut.glutPostRedisplay(); // Add this line to force a constant redrawing of the scene
        }

        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
        }

        static void TeclasEspeciais(int key, int x, int y)
        {
            switch (key)
            {
                case Glut.GLUT_KEY_LEFT:
                    if (carPositionX > -0.9f) // Check if the car is within the left boundary
                    {
                        rot += 10.0f;
                        carPositionX -= 0.05f;
                    }
                    break;

                case Glut.GLUT_KEY_RIGHT:
                    if (carPositionX < 0.9f) // Check if the car is within the right boundary
                    {
                        rot -= 10.0f;
                        carPositionX += 0.05f;
                    }
                    break;

                case Glut.GLUT_KEY_UP: // Add case for the up arrow key
                    break;

                case Glut.GLUT_KEY_DOWN: // Add case for the down arrow key
                    break;
            }

            Glut.glutPostRedisplay();
        }


        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(800, 600);
            Glut.glutCreateWindow("Projeto");

            Glut.glutReshapeFunc(OnReshape);

            Glut.glutDisplayFunc(display);
            Glut.glutSpecialFunc(TeclasEspeciais);
            inicializa();
            Glut.glutMainLoop();
        }

        private static void OnReshape(int width, int height)
        {
            Gl.glViewport(0, 0, width, height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            float aspectRatio = (float)width / (float)height;

            if (width <= height)
            {
                Gl.glOrtho(-1.0, 1.0, -1.0 / aspectRatio, 1.0 / aspectRatio, -1.0, 1.0);
            }
            else
            {
                Gl.glOrtho(-1.0 * aspectRatio, 1.0 * aspectRatio, -1.0, 1.0, -1.0, 1.0);
            }

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
    }
}
