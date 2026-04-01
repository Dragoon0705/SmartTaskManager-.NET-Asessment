import pandas as pd
import pickle
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression, LogisticRegression
from sklearn.metrics import mean_absolute_error, mean_squared_error, accuracy_score, precision_score, recall_score, f1_score
from preprocessing import load_and_clean

df, _, _, _ = load_and_clean()

# ------------------ REGRESSION ------------------
X_reg = df[["TaskCategory", "EstimatedDuration", "Priority"]]
y_reg = df["ActualDuration"]

X_train, X_test, y_train, y_test = train_test_split(X_reg, y_reg, test_size=0.2)

reg_model = LinearRegression()
reg_model.fit(X_train, y_train)

y_pred = reg_model.predict(X_test)

print("Regression MAE:", mean_absolute_error(y_test, y_pred))
rmse = mean_squared_error(y_test, y_pred) ** 0.5
print("Regression RMSE:", rmse)

pickle.dump(reg_model, open("models/regression.pkl", "wb"))

# ------------------ CLASSIFICATION ------------------
X_clf = df[["TaskCategory", "EstimatedDuration", "Priority"]]
y_clf = df["CompletionStatus"]

X_train, X_test, y_train, y_test = train_test_split(X_clf, y_clf, test_size=0.2)

clf_model = LogisticRegression()
clf_model.fit(X_train, y_train)

y_pred = clf_model.predict(X_test)

print("Accuracy:", accuracy_score(y_test, y_pred))
print("Precision:", precision_score(y_test, y_pred))
print("Recall:", recall_score(y_test, y_pred))
print("F1:", f1_score(y_test, y_pred))

pickle.dump(clf_model, open("models/classification.pkl", "wb"))