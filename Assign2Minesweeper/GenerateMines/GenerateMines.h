#ifndef GENERATEMINES_H
#define GENERATEMINES_H
#include <random>
#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <algorithm>
class GenerateMines
{
	friend inline std::ostream& operator<<(std::ostream& out, const GenerateMines& mines)
	{
		for (const auto& i : mines.m_mineField)
		{
			for (const auto& square:i)
			{
				if (square)
				{
					out << "*";
				}
				else
				{
					out << ".";
				}
			}
			out << "\n";
		}
		return out;
	}
public:
	GenerateMines();
	virtual ~GenerateMines();
	void generateField(const int x, const int y);
private:
	std::vector<std::vector<bool>> m_mineField;
};
#endif
