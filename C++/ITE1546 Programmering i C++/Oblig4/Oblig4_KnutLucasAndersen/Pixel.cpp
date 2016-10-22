#include <iostream>
#include "Pixel.h";

using namespace std;

Pixel::Pixel(int r, int g, int b) {
	Pixel::r = r;
	Pixel::g = g;
	Pixel::b = b;
} //konstruktør

void Pixel::edit(int r, int g, int b) {
	Pixel::r = r;
	Pixel::g = g;
	Pixel::b = b;
} //edit

int Pixel::getR() {
	return r;
} //getR

int Pixel::getG() {
	return g;
} //getG

int Pixel::getB() {
	return b;
} //getB