//compiles with VS 2015 and g++5
#include <cstdlib>
#include <utility>
#include "GenerateMines.h"
#define ARRAY_MAX 100

int main()
{
	GenerateMines mines;
	
	std::ofstream fout;
	fout.open("out.txt", fout.out);
	for (int i = ARRAY_MAX;i >=0;i--)
	{
		for (int j = ARRAY_MAX;j >=0;j--)
		{
			fout << i << " " << j << std::endl;
			mines.generateField(i, j);
			fout << mines;
		}
	}
	fout.close();
	return EXIT_SUCCESS;
}
