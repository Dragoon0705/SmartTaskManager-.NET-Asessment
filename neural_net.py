import tensorflow as tf
from preprocessing import load_and_clean
from sklearn.model_selection import train_test_split

df, _, _, _ = load_and_clean()

X = df[["TaskCategory", "EstimatedDuration", "Priority"]].values
y = df["CompletionStatus"].values

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2)

model = tf.keras.Sequential([
    tf.keras.layers.Dense(8, activation='relu'),
    tf.keras.layers.Dense(4, activation='relu'),
    tf.keras.layers.Dense(1, activation='sigmoid')
])

model.compile(optimizer='adam',
              loss='binary_crossentropy',
              metrics=['accuracy'])

model.fit(X_train, y_train, epochs=50)

loss, acc = model.evaluate(X_test, y_test)
print("Neural Network Accuracy:", acc)