#include <SDL.h>
#include <stdio.h>
int main() {

#pragma region test_init

    if (SDL_Init(SDL_INIT_VIDEO) < 0) {
        fprintf(stderr, "SDL could not initialise! SDL_Error: %d\n", SDL_GetError());
        return 1;
    }

    SDL_Window* window = SDL_CreateWindow("SDL Tutorial", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL_WINDOW_SHOWN);
    if (!window) {
        fprintf(stderr, "Window could not be created! SDL_Error: %d\n", SDL_GetError());
        return 2;
    }

    SDL_DestroyWindow(window);
    SDL_Quit();
    return 0;
#pragma endregion
}
