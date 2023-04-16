using System;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.Drawing;
using static Projeto.Program;

namespace Projeto
{
    class Program
    {
        static float rot = 0.0f;
        static float carPositionX = 0.0f;
        const float PI = 3.14f;
        static int step = 0;
        static int horario = 0;
        static CorFundo corFundo = new CorFundo(r: 0.086f, g: 0.447f, b: 0.698f);
        static float posicaoNuvens1 = 0.0f;
        static float posicaoNuvens2 = 0.5f;
        static float posicaoNuvens3 = -0.6f;
        static float posicaoNuvens4 = -0.3f;
        static float posicaoNuvens5 = 0.4f;
        static float posicaoNuvens6 = 0.7f;
        const int width = 1200;
        const int height = 800;


        static void desenhaCirculo(bool isFrontWheel)
        {
            float positionX = carPositionX;
            if (isFrontWheel)
            {
                positionX += 0.2f;
            }
            else
            {
                positionX += -0.2f;
            }

            Gl.glPushMatrix();
            Gl.glTranslatef(positionX, 0.0f, 0.0f);
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);
            Gl.glScalef(0.5f, 0.5f, 0.5f);

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
            Gl.glScalef(0.5f, 0.5f, 0.5f);

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

            fundo(corFundo);

            DesenharVariasNuvens();

            desenhaCirculo(true);
            desenhaCirculo(false);
            desenhaCarro();



            Glut.glutSwapBuffers();
            Glut.glutPostRedisplay(); // Add this line to force a constant redrawing of the scene
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


        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("Carro");
            Glut.glutSpecialFunc(TeclasEspeciais);
            Glut.glutDisplayFunc(display);
            Glut.glutTimerFunc(40, Timer, 1);
            Glut.glutTimerFunc(40, TimerNuvem, 1);
            Glut.glutReshapeFunc(OnReshape);
            inicializa();
            Glut.glutMainLoop();
        }

        static void desenha()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            fundo(corFundo);

            DesenharVariasNuvens();

            Glut.glutSwapBuffers();
        }

        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, 1.0, 0.0, 1.0);
            Gl.glClearColor(1, 1, 1, 0);
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
        static void fundo(CorFundo corFundo)
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            ConfigurarHorarioDia();

            DesenharRodovia();
        }

        static void TimerNuvem(int value)
        {
            //TODO - Melhorando isso com um laço de repetição
            AlterandoPosicaoNuvens();

            Glut.glutPostRedisplay();
            Glut.glutTimerFunc(10, TimerNuvem, 1);
        }

        static void Timer(int value)
        {
            const int num_steps = 100; // número de passos para a transição completa
            float t = (float)step / num_steps; // progresso da transição, entre 0 e 1
            CorFundo corAnterior = corFundo;
            CorFundo corNova = null;

            switch (horario)
            {
                case 0: // Dia para tarde
                    if (step < num_steps / 2)
                        corNova = LerpCor(corAnterior, new CorFundo(r: 0.0f, g: 0.74902f, b: 1.0f), t);
                    else
                        corNova = LerpCor(corAnterior, new CorFundo(r: 0.086f, g: 0.447f, b: 0.698f), t);
                    break;
                case 1: // Tarde para noite
                    corNova = LerpCor(corAnterior, new CorFundo(r: 0.086f, g: 0.447f, b: 0.698f), t);
                    break;
                case 2: // Noite para dia
                    corNova = LerpCor(corAnterior, new CorFundo(r: 0.0f, g: 0.0f, b: 0.0f), t);
                    break;
            }

            if (corNova != null)
            {
                corFundo = corNova;
                fundo(corFundo);
                Glut.glutPostRedisplay();
            }

            step++;
            if (step >= num_steps)
            {
                step = 0;
                horario = (horario + 1) % 3;
            }

            Glut.glutTimerFunc(50, Timer, 1);
        }
        static CorFundo LerpCor(CorFundo corFundoA, CorFundo corFundoB, float t)
        {
            float r = (1 - t) * corFundoA.R + t * corFundoB.R;
            float g = (1 - t) * corFundoA.G + t * corFundoB.G;
            float b = (1 - t) * corFundoA.B + t * corFundoB.B;
            return new CorFundo(r, g, b);
        }

        public class CorFundo
        {
            public CorFundo(float r, float g, float b)
            {
                R = r;
                G = g;
                B = b;
            }

            public float R { get; set; }
            public float G { get; set; }
            public float B { get; set; }
        }

        static void AlterandoPosicaoNuvens()
        {
            posicaoNuvens1 += 0.001f;
            if (posicaoNuvens1 > 1.0f)
            {
                posicaoNuvens1 = -0.5f;
            }

            posicaoNuvens2 += 0.0015f;
            if (posicaoNuvens2 > 1.5f)
            {
                posicaoNuvens2 = -0.5f;
            }

            posicaoNuvens3 += 0.0012f;
            if (posicaoNuvens3 > 1.2f)
            {
                posicaoNuvens3 = -0.4f;
            }

            posicaoNuvens4 += 0.0013f;
            if (posicaoNuvens4 > 1.3f)
            {
                posicaoNuvens4 = -0.3f;
            }

            posicaoNuvens5 += 0.0014f;
            if (posicaoNuvens5 > 1.4f)
            {
                posicaoNuvens5 = -0.2f;
            }

            posicaoNuvens6 += 0.0016f;
            if (posicaoNuvens6 > 1.6f)
            {
                posicaoNuvens6 = -0.6f;
            }
        }

        static void DesenharVariasNuvens()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens1, 0.8f, 0.0f);
            Gl.glScalef(0.2f, 0.2f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens2, 0.7f, 0.0f);
            Gl.glScalef(0.15f, 0.15f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens3, 0.9f, 0.0f);
            Gl.glScalef(0.25f, 0.25f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens4, 0.7f, 0.0f);
            Gl.glScalef(0.2f, 0.2f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens5, 0.85f, 0.0f);
            Gl.glScalef(0.3f, 0.3f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(posicaoNuvens6, 0.75f, 0.0f);
            Gl.glScalef(0.25f, 0.25f, 1.0f);
            desenhaNuvem();
            Gl.glPopMatrix();
        }

        static void ConfigurarHorarioDia()
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.282353f, 0.239216f, 0.545098f);
            Gl.glVertex2f(0, height); // Top-left
            Gl.glVertex2f(width, height); // Top-right

            Gl.glColor3f(corFundo.R, corFundo.G, corFundo.B);
            Gl.glVertex2f(width, 0); // Bottom-right
            Gl.glVertex2f(0, 0); // Bottom-left
            Gl.glEnd();

            configurarAstroCeu();
        }

        static void DesenharRodovia()
        {
            int windowWidth = width;
            int windowHeight = height;

            float relativeWidth = (float)windowWidth / (float)windowHeight;

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex2f(relativeWidth, 0.25f);
            Gl.glVertex2f(0.0f, 0.25f);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(relativeWidth, 0.0f);

            Gl.glColor3f(1.0f, 1.0f, 0.0f); // amarelo

            float lineWidth = 0.11f * relativeWidth; // largura da linha
            float gapWidth = 0.05f * relativeWidth; // largura do espaço entre as linhas
            int numLines = (int)(7 * relativeWidth); // número de linhas que serão desenhadas

            // loop para criar as linhas tracejadas
            for (int i = 0; i < numLines; i++)
            {
                float startX = (i * (lineWidth + gapWidth)); // posição inicial da linha
                float endX = startX + lineWidth; // posição final da linha

                // desenha a linha amarela
                Gl.glVertex2f(startX + lineWidth / 2, 0.125f);
                Gl.glVertex2f(startX, 0.125f);
                Gl.glVertex2f(startX, 0.1f);
                Gl.glVertex2f(startX + lineWidth / 2, 0.1f);

                // desenha o espaço entre as linhas
                Gl.glColor3f(0.0f, 0.0f, 0.0f);
                Gl.glVertex2f(endX, 0.125f);
                Gl.glVertex2f(endX - gapWidth / 2, 0.125f);
                Gl.glVertex2f(endX - gapWidth / 2, 0.1f);
                Gl.glVertex2f(endX, 0.1f);

                // volta para a cor amarela para desenhar a próxima linha
                Gl.glColor3f(1.0f, 1.0f, 0.0f);
            }

            Gl.glEnd();
        }
        static void configurarAstroCeu()
        {
            if (horario == 1 || horario == 2)
                Gl.glColor3f(0.8f, 0.8f, 0.8f); //SOL - AMARELO
            else
                Gl.glColor3f(1.0f, 1.0f, 0.0f); //LUA - CINZA CLARO

            Gl.glPushMatrix();
            Gl.glTranslatef(0.5f, 0.9f, 0.0f);
            Glu.gluDisk(Glu.gluNewQuadric(), 0, 0.15f, 32, 1); //Pelo que entendi, desenha um circulo com raio de 0.1f e 32 de segmento
            Gl.glPopMatrix();
        }


        //TODO - Desenhar uma nuvem com mais detalhes
        static void desenhaNuvem()
        {
            Gl.glColor3f(1.0f, 1.0f, 1.0f);

            //Faço 4 circulos aqui praticamentes juntos mas separados na localidades. 
            Gl.glPushMatrix();
            Gl.glTranslatef(-0.2f, 0.2f, 0.0f);
            Glut.glutSolidSphere(0.15f, 20, 20);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(-0.1f, 0.3f, 0.0f);
            Glut.glutSolidSphere(0.2f, 20, 20);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(0.1f, 0.3f, 0.0f);
            Glut.glutSolidSphere(0.2f, 20, 20);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glTranslatef(0.2f, 0.2f, 0.0f);
            Glut.glutSolidSphere(0.15f, 20, 20);
            Gl.glPopMatrix();
        }
    }
}
