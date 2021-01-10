import numpy as np
import matplotlib.pyplot as plt


def plot(path):
    chunk = np.loadtxt(path)
    # chunk = np.genfromtxt(path, missing_values="NaN")
    data = np.array(chunk)
    Xs = data[:, 0]
    Ys = data[:, 1]
    Zs = data[:, 2]
    Pys = data[:, 3]
    Pzs = data[:, 4]

    fig = plt.figure()

    som = fig.add_subplot(111)

    plt.plot(Xs, Ys, 'b', label="X1")
    plt.plot(Xs, Zs, 'r', label="X2")
    plt.plot(Xs, Pys, 'g', label="Pendulum X1")
    plt.plot(Xs, Pys, 'm', label="Pendulum X2")
    plt.legend()

    fig.tight_layout()
    plt.show()


def main():
    root = "C:/git/Computer-Aided-Analysis-and-Design/Homework_5/Files/"
    assignment = 'Assignment' + str(1) + '/'
    method = 'euler.txt'
    test = root + assignment + method
    print(test)
    plot(test)


if __name__ == '__main__':
    main()
