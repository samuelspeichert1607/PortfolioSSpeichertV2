template <class T>
Noeud<T>::Noeud()
{

}
template <class T>
Noeud<T>::~Noeud()
{

}
template <class T>
Noeud<T>* Noeud<T>::getSuivant()
{
	return prochainNoeud;
}
template <class T>
Noeud<T>* Noeud<T>::getPrevious()
{
	return noeudPrecedant;
}
template <class T>
void Noeud<T>::setSuivant(Noeud<T>* _suivant)
{
	prochainNoeud = _suivant;
}
template <class T>
void Noeud<T>::setPrevious(Noeud<T>* _previous)
{
	noeudPrecedant = _previous;
}
template <class T>
T* Noeud<T>::getContenu()
{
	return contenu;
}
template <class T>
void Noeud<T>::setContenu(T* _contenu)
{
	contenu = _contenu;
}
