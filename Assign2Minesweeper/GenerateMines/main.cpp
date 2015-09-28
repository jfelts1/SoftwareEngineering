//compiles with VS 2015
#include <cstdlib>
#include <utility>
#include "GenerateMines.h"
#define ARRAY_MAX 100

int main()
{
	GenerateMines mines;
	
	std::ofstream fout;
	fout.open("out.txt", fout.out);
	for (int i = 0;i <= ARRAY_MAX;i++)
	{
		for (int j = 0;j <= ARRAY_MAX;j++)
		{
			fout << i << " " << j << std::endl;
			mines.generateField(i, j);
			fout << mines;
		}
	}
	fout.close();
	return EXIT_SUCCESS;
}